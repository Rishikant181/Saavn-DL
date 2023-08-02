namespace Saavn {
    /// <summary>
    /// The details of a playlist.
    /// </summary>
    public class Playlist {
        /// <summary> The name of the playlist </summary>
        private readonly string _name;

        /// <summary> The list of music tracks in the playlist </summary>
        private readonly List<Music> _tracks;

        /// <summary>
        /// Initializes a new playlist from the playlist with given url.
        /// </summary>
        ///
        /// <param name="url"> The playlist data URL </param>
        public Playlist(string url) {
            // Getting the playlist id from url
            string id = url.Substring(url.LastIndexOf('/') + 1);

            // Getting the raw playlist
            Types.Raw.Playlist playlist = Utility.Http.FetchResource<Types.Raw.Playlist>(ResourceType.PLAYLIST , id);

            // Initializing the playlist
            this._name = playlist.title;
            this._tracks = playlist.list.Select(track => new Music(track)).ToList();
        }

        /// <summary>
        /// Downloads each song in the playlist.
        /// <summary>
        public void Download() {
            foreach (Music music in this._tracks) {
                music.Download(this._name);
            }
        }
    }

}