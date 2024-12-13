namespace GetMediaCore.Models;

public partial class Album
{
    public int AlbumId { get; set; }

    public int AlbumArtistId { get; set; }

    public string? AlbumTitle { get; set; }

    public int? AlbumGenre { get; set; }

    public int? AlbumYear { get; set; }

    public int? AlbumDisc { get; set; }
}
