using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace GetMediaCore.Models;

public partial class ArtistAlbumSongXref
{
    public int RefId { get; set; }

    public int ArtistId { get; set; }

    public int AlbumId { get; set; }

    public int SongId { get; set; }
}
