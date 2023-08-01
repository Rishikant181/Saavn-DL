namespace ParsedData {
    /// <summary>
    /// The details of an Artist.
    /// </summary>
    public class Artist {
        /// <summary> The id of the artist </summary>
        private string id;

        /// <summary> The name of the artist </summary>
        public string name;

        /// <summary> The role of the artist </summary>
        public string role;

        /// <summary> The URL to the image of the artist </summary>
        public string image;

        /// <summary>
        /// Initializes a new Artist from the given raw artist info.
        /// </summary>
        ///
        /// <param name = "artist">The raw data of the artist</param>
        public Artist(RawData.Artist artist) {
            this.id = artist.id;
            this.name = artist.name;
            this.role = artist.role;
            this.image = artist.image;
        }
    }
}