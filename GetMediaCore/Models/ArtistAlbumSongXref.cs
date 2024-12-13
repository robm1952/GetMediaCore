namespace GetMediaCore.Models;

public partial class ArtistAlbumSongXref
{
    public ArtistAlbumSongXref(Int32 ArtistId, Int32 AlbumId, Int32 SongId)
    {
        this.ArtistId = ArtistId;
        this.AlbumId = AlbumId;
        this.SongId = SongId;
    }
    public int RefId { get; set; }

    public int ArtistId { get; set; }

    public int AlbumId { get; set; }

    public int SongId { get; set; }

}
