using TagLib;
using Xabe.FFmpeg;

namespace Saavn {
    namespace Resources {
        /// <summary>
        /// The details of a music.
        /// </summary>
        public class Music {
            /// <summary> The title of the music </summary>
            private readonly string _title;

            /// <summary> The URL to the album art of the music </summary>
            private readonly string _albumArtUrl;

            /// <summary> The year in which this music was released </summary>
            private readonly uint _year;

            /// <summary> The name of the album to which this music belongs </summary>
            private readonly string _album;

            /// <summary> The label under which the music was released </summary>
            private readonly string _label;

            /// <summary> The media url to the music </summary>
            private string _mediaUrl;

            /// <summary> The copyright text on the music </summary>
            private readonly string _copyright;

            /// <summary> The list of primary contributing artists </summary>
            private readonly List<Artist> _contributingArtists;

            /// <summary>
            /// Initializes a new Music from the music with the given id.
            /// </summary>
            ///
            /// <param name="id"> The id of the music. </param>
            public Music(string id) {
                // Fetching the raw music data
                Types.Raw.Music music = Utility.Http.FetchResource<Types.Raw.MusicResponse>(ResourceType.MUSIC , id).songs[0];

                // Initializing from the raw music data
                this._title = music.title;
                this._albumArtUrl = music.image.Replace("150x150.jpg" , "500x500.jpg");
                this._year = UInt16.Parse(music.year);
                this._album = music.more_info.album;
                this._label = music.more_info.label;
                this._mediaUrl = music.more_info.encrypted_media_url;
                this._copyright = music.more_info.copyright_text;
                this._contributingArtists = music.more_info.artistMap.primary_artists.Select(artist => new Artist(artist)).ToList();
            }

            /// <summary>
            /// Initializes a new Music from the raw music data.
            /// </summary>
            ///
            /// <param name = "music"> The raw music data </param>
            public Music(Types.Raw.Music music) {
                this._title = music.title;
                this._albumArtUrl = music.image.Replace("150x150.jpg" , "500x500.jpg");
                this._year = UInt16.Parse(music.year);
                this._album = music.more_info.album;
                this._label = music.more_info.label;
                this._mediaUrl = music.more_info.encrypted_media_url;
                this._copyright = music.more_info.copyright_text;
                this._contributingArtists = music.more_info.artistMap.primary_artists.Select(artist => new Artist(artist)).ToList();
            }

            /// <summary>
            /// Appends the metadata of 'this' music to the specified file.
            /// </summary>
            ///
            /// <param name="fileName"> The full name of the music file to which metadata is to be appended </param>
            private void AppendMetadata(string fileName) {
                // Configuring taglib
                TagLib.Id3v2.Tag.DefaultVersion = 3;
                TagLib.Id3v2.Tag.ForceDefaultVersion = true;

                // Opening the music file
                TagLib.File music = TagLib.File.Create(fileName);

                // Adding tags
                music.Tag.Album = this._album;
                music.Tag.Performers = this._contributingArtists.Select(artist => artist.Name).ToArray<string>();
                music.Tag.Copyright = this._copyright;
                music.Tag.Title = this._title;
                music.Tag.Year = this._year;
                music.Tag.Pictures = new IPicture[] {
                    new TagLib.Id3v2.AttachmentFrame() {
                        Type = PictureType.FrontCover,
                        Data = ByteVector.FromStream(Program.client.GetAsync(this._albumArtUrl).GetAwaiter().GetResult().Content.ReadAsStream())
                    }
                };
                music.Tag.Publisher = this._label;

                // Saving the file
                music.Save();
            }

            /// <summary>
            /// Downloads the music to the given location.
            /// </summary>
            ///
            /// <param name = "location"> The location where the music is to be saved </param>
            public void Download(string location) {
                // The full name of the music file
                string fileName = $"{location}\\{this._album} - {this._title}.mp3";

                // Getting the direct media URL
                this._mediaUrl = Utility.Http.FetchResource<Types.Raw.MediaUrl>(ResourceType.MEDIA_URL , this._mediaUrl).auth_url;

                // Creating a conversion task
                IConversion conversionTask = FFmpeg.Conversions.FromSnippet.Convert(this._mediaUrl , fileName).GetAwaiter().GetResult();

                // Setting the bitrate
                conversionTask.SetAudioBitrate(320000);

                // Converting
                conversionTask.Start().GetAwaiter().GetResult();

                // Appending metadata
                this.AppendMetadata(fileName);
            }
        }
    }

}
