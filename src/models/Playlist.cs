using Newtonsoft.Json;

namespace Models {
    namespace Saavn {
        /// <summary>
        /// The details of a playlist.
        /// </summary>
        public class Playlist {
            /// <summary> The id of the playlist </summary>
            private string Id;

            /// <summary> The name of the playlist </summary>
            private string Name;

            /// <summary> The list of music tracks in the playlist </summary>
            private List<Music> Tracks;

            /// <summary>
            /// Initializes a new playlist from the playlist with given url.
            /// </summary>
            ///
            /// <param name="url"> The playlist data URL </param>
            public Playlist(string url) {
                // Getting the playlist id from url
                string id = url.Substring(url.LastIndexOf('/') + 1);

                // Getting the raw playlist
                Types.Response.Playlist playlist = this.GetRawPlayList(id);

                // Initializing the playlist
                this.Id = playlist.id;
                this.Name = playlist.title;
                this.Tracks = playlist.list.Select(track => new Music(track)).ToList();
            }

            /// <summary>
            /// Fetches the raw playlist details with the given id.
            /// </summary>
            ///
            /// <param name="id"> The id of the playlist </param>
            private Types.Response.Playlist GetRawPlayList(string id) {
                // Preparing the HTTP request
                HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get ,
                    $"https://www.jiosaavn.com/api.php?__call=webapi.get&token={id}&type=playlist&p=1&n=9999999999&includeMetaTags=0&ctx=web6dot0&api_version=4&_format=json&_marker=0"
                );

                // Sending the HTTP request and getting the response
                string response = Program.client.Send(request).Content.ReadAsStringAsync().Result;

                // Deserializing the response
                Types.Response.Playlist rawPlaylist = JsonConvert.DeserializeObject<Types.Response.Playlist>(response)!;

                return rawPlaylist;
            }

            /// <summary>
            /// Downloads each song in the playlist.
            /// <summary>
            public void Download() {
                foreach (Music music in this.Tracks) {
                    music.Download(this.Name);
                }
            }
        }

    }
}