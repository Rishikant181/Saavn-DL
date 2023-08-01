using Newtonsoft.Json;

namespace ParsedData {
    public class Music {
        /// <summary> The id of the music </summary>
        private string id;

        /// <summary> The title of the music </summary>
        public string title;

        /// <summary> The URL to the album art of the music </summary>
        public string albumArtUrl;

        /// <summary> The language of the music </summary>
        public string language;

        /// <summary> The year in which this music was released </summary>
        public string year;

        /// <summary> The name of the album to which this music belongs </summary>
        public string album;

        /// <summary> The label under which the music was released </summary>
        public string label;

        /// <summary> The media url to the music </summary>
        public string mediaUrl;

        /// <summary> The copyright text on the music </summary>
        public string copyright;

        /// <summary> The list of primary contributing artists </summary>
        public List<Artist> contributingArtists;

        /// <summary>
        /// Initializes a new Music from the raw music data.
        /// </summary>
        ///
        /// <param name = "music">The raw music data</param>
        public Music(RawData.Music music) {
            this.id = music.id;
            this.title = music.title;
            this.albumArtUrl = music.image.Replace("150x150.jpg", "500x500.jpg");
            this.language = music.language;
            this.year = music.year;
            this.album = music.more_info.album;
            this.label = music.more_info.label;
            this.mediaUrl = music.more_info.encrypted_media_url;
            this.copyright = music.more_info.copyright_text;
            this.contributingArtists = music.more_info.artistMap.primary_artists.Select(artist => new Artist(artist)).ToList();
        }

        /// <summary>
        /// Fetches the direct media URL to the music and stored it in 'this' object.
        /// </summary>
        public async Task GetMediaUrl() {
            // Preparing the HTTP request
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://www.jiosaavn.com/api.php?__call=song.generateAuthToken&url={this.mediaUrl}&bitrate=320&api_version=4&_format=json&ctx=web6dot0&_marker=0"
            );

            // Sending the HTTP request and getting the response
            string response = await Program.client.Send(request).Content.ReadAsStringAsync();

            // Deserializing the response and storing the URL
            this.mediaUrl = JsonConvert.DeserializeObject<RawData.MediaUrl>(response)!.auth_url;
        }
    }
}