using System;
using System.Collections.Generic;

namespace GetMediaCore.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string? ArtistName { get; set; }

    public string? ArtistSortName { get; set; }
}
