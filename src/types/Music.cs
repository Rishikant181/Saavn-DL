namespace Types {
    namespace Raw {
        /// <summary>
        /// The raw data received while fetching details of a music.
        /// </summary>
        public class MusicResponse {
            public List<Music> songs = default!;
        }

        /// <summary>
        /// The raw data of a music.
        /// </summary>
        public class Music {
            public string id = default!;
            public string title = default!;
            public string image = default!;
            public string language = default!;
            public string year = default!;
            public ExtendedMusicInfo more_info = default!;
        }

        /// <summary>
        /// Additional raw data about a music.
        /// </summary>
        public class ExtendedMusicInfo {
            public string music = default!;
            public string album_id = default!;
            public string album = default!;
            public string label = default!;
            public string encrypted_media_url = default!;
            public string copyright_text = default!;
            public ArtistMap artistMap = default!;
            public string release_date = default!;
        }

        /// <summary>
        /// The raw data of contributing artists of the music.
        /// </summary>
        public class ArtistMap {
            public List<Artist> primary_artists = default!;
            public List<Artist> featured_artists = default!;
            public List<Artist> artists = default!;
        }
    }
}
