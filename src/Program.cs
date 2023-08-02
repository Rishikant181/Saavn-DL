class Program {
    /// <summary>
    /// Downloads the music/playlist with the given URL.
    /// </summary>
    ///
    /// <param name="url"> The URL to the music/playlist </param>
    private static void DowloadFromUrl(string url) {
        // Getting the id of the music/playlist
        string id = url.Substring(url.LastIndexOf('/') + 1);

        // If URL is music URL
        if (url.Contains("/song/")) {
            new Saavn.Resources.Music(id).Download(".");
        }
        // If URL is playlist URL
        else if (url.Contains("/playlist/")) {
            new Saavn.Resources.Playlist(id).Download();
        }
    }

    /// <summary>
    /// The entry point of this app.
    /// </summary>
    ///
    /// <param name="args"> Commandline arguments </param>
    public static void Main(string[] args) {
        // Getting the URL to download
        string url = args[0];

        // Downloading
        DowloadFromUrl(url);
    }
}
