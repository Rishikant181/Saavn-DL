using Newtonsoft.Json;
using Xabe.FFmpeg;
using TagLib;

namespace ParsedData {
    /// <summary>
    /// The details of a music.
    /// </summary>
    public class Music {
        /// <summary> The id of the music </summary>
        private string Id;

        /// <summary> The title of the music </summary>
        private string Title;

        /// <summary> The URL to the album art of the music </summary>
        private string AlbumArtUrl;

        /// <summary> The language of the music </summary>
        private string Language;

        /// <summary> The year in which this music was released </summary>
        private uint Year;

        /// <summary> The name of the album to which this music belongs </summary>
        private string Album;

        /// <summary> The label under which the music was released </summary>
        private string Label;

        /// <summary> The media url to the music </summary>
        private string MediaUrl;

        /// <summary> The copyright text on the music </summary>
        private string Copyright;

        /// <summary> The list of primary contributing artists </summary>
        private List<Artist> ContributingArtists;

        /// <summary>
        /// Initializes a new Music from the raw music data.
        /// </summary>
        ///
        /// <param name = "music">The raw music data</param>
        public Music(RawData.Music music) {
            this.Id = music.id;
            this.Title = music.title;
            this.AlbumArtUrl = music.image.Replace("150x150.jpg", "500x500.jpg");
            this.Language = music.language;
            this.Year = UInt16.Parse(music.year);
            this.Album = music.more_info.album;
            this.Label = music.more_info.label;
            this.MediaUrl = music.more_info.encrypted_media_url;
            this.Copyright = music.more_info.copyright_text;
            this.ContributingArtists = music.more_info.artistMap.primary_artists.Select(artist => new Artist(artist)).ToList();
        }

        /// <summary>
        /// Fetches the direct media URL to the music and stored it in 'this' object.
        /// </summary>
        private void GetMediaUrl() {
            // Preparing the HTTP request
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://www.jiosaavn.com/api.php?__call=song.generateAuthToken&url={Uri.EscapeDataString(this.MediaUrl)}&bitrate=320&api_version=4&_format=json&ctx=web6dot0&_marker=0"
            );

            // Sending the HTTP request and getting the response
            string response = Program.client.Send(request).Content.ReadAsStringAsync().Result;

            // Deserializing the response and storing the URL
            this.MediaUrl = JsonConvert.DeserializeObject<RawData.MediaUrl>(response)!.auth_url;
        }

        /// <summary>
        /// Appends the metadata of 'this' music to the specified file.
        /// </summary>
        ///
        /// <param name="fileName">The full name of the music file to which metadata is to be appended</param>
        private void AppendMetadata(string fileName) {
            // Opening the music file
            TagLib.File music = TagLib.File.Create(fileName);

            // Adding tags
            music.Tag.Album = this.Album;
            music.Tag.Performers = this.ContributingArtists.Select(artist => artist.Name).ToArray<string>();
            music.Tag.Copyright = this.Copyright;
            music.Tag.Title = this.Title;
            music.Tag.Year = this.Year;
            music.Tag.Pictures = new TagLib.IPicture[] {
                new TagLib.Picture() {
                    Type = TagLib.PictureType.FrontCover,
                    Data = TagLib.ByteVector.FromStream(Program.client.GetAsync(this.AlbumArtUrl).GetAwaiter().GetResult().Content.ReadAsStream())
                }
            };

            // Saving the file
            music.Save();
        }

        /// <summary>
        /// Downloads the music to the given location.
        /// </summary>
        ///
        /// <param name = "location">The location where the music is to be saved</param>
        public void Download(string location) {
            // The full name of the music file
            string fileName = $"{location}\\{this.Album} - {this.Title}.mp3";

            // Getting the media url
            this.GetMediaUrl();

            // Creating a conversion task
            var conversionTask = FFmpeg.Conversions.FromSnippet.Convert(this.MediaUrl, fileName).GetAwaiter().GetResult();

            // Setting the bitrate
            conversionTask.SetAudioBitrate(320000);

            // Converting
            conversionTask.Start().GetAwaiter().GetResult();

            // Appending metadata
            this.AppendMetadata(fileName);
        }
    }
}