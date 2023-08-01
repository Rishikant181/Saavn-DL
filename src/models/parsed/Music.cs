using Newtonsoft.Json;

namespace ParsedData {
    /// <summary>
    /// The details of a music.
    /// </summary>
    public class Music {
        /// <summary> The id of the music </summary>
        private string Id;

        /// <summary> The title of the music </summary>
        public string Title;

        /// <summary> The URL to the album art of the music </summary>
        public string AlbumArtUrl;

        /// <summary> The language of the music </summary>
        public string Language;

        /// <summary> The year in which this music was released </summary>
        public string Year;

        /// <summary> The name of the album to which this music belongs </summary>
        public string Album;

        /// <summary> The label under which the music was released </summary>
        public string Label;

        /// <summary> The media url to the music </summary>
        public string MediaUrl;

        /// <summary> The copyright text on the music </summary>
        public string Copyright;

        /// <summary> The list of primary contributing artists </summary>
        public List<Artist> ContributingArtists;

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
            this.Year = music.year;
            this.Album = music.more_info.album;
            this.Label = music.more_info.label;
            this.MediaUrl = music.more_info.encrypted_media_url;
            this.Copyright = music.more_info.copyright_text;
            this.ContributingArtists = music.more_info.artistMap.primary_artists.Select(artist => new Artist(artist)).ToList();
        }

        /// <summary>
        /// Fetches the direct media URL to the music and stored it in 'this' object.
        /// </summary>
        public void GetMediaUrl() {
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
    }
}