namespace GetMediaCore.Models;

public partial class Genre
{
    public Genre() { }

    public Genre(int id, string name)
    {
        GenreId = id;
        GenreName = name;
    }
    public int GenreId { get; set; }

    public string? GenreName { get; set; }
}
