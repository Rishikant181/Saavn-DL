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
        // The URL to the song/playlist to download
        string url = "";

        // If no URL is provided as argument, asking for one
        if (args.Length == 0) {
            Console.Write("Enter URL of the song/playlist to download: ");
            url = Console.ReadLine() ?? "";
        }
        // If URL is provided as argument
        else {
            url = args[0];
        }

        // If invalid URL provided
        if (url.Length == 0) {
            Console.WriteLine("Please Enter a valid URL!");
            return;
        }

        // Downloading
        DowloadFromUrl(url);

        // Informing of download completion
        Console.Write("\nDownload complete! Press any key to continue ");
        Console.ReadKey();
    }
}
