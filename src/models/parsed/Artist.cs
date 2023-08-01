/// <summary>
/// The details of an Artist.
/// </summary>
public class Artist
{
    /// <summary> The id of the artist </summary>
    private string Id;

    /// <summary> The name of the artist </summary>
    public string Name;

    /// <summary> The role of the artist </summary>
    public string Role;

    /// <summary> The URL to the image of the artist </summary>
    public string Image;

    /// <summary>
    /// Initializes a new Artist from the given raw artist info.
    /// </summary>
    ///
    /// <param name = "artist"> The raw data of the artist </param>
    public Artist(RawData.Artist artist)
    {
        this.Id = artist.id;
        this.Name = artist.name;
        this.Role = artist.role;
        this.Image = artist.image;
    }
}
