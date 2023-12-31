namespace Types {
    namespace Raw {
        /// <summary>
        /// The raw data of a playlist.
        /// </summary>
        public class Playlist {
            public string id = default!;
            public string title = default!;
            public string list_count = default!;
            public List<Music> list = default!;
        }
    }
}
