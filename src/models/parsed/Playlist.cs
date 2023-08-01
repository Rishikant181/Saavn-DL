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
    /// Initializes a new playlist from the raw playlist data.
    /// </summary>
    ///
    /// <param name="playlist"> The raw playlist data </param>
    public Playlist(RawData.Playlist playlist) {
        this.Id = playlist.id;
        this.Name = playlist.title;
        this.Tracks = playlist.list.Select(track => new Music(track)).ToList();
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