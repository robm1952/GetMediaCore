using System;
using System.Collections.Generic;

namespace GetMediaCore.Models;

public partial class PlaylistHistory
{
    public int M3uid { get; set; }

    public string? Artist { get; set; }

    public string? Album { get; set; }

    public int? Year { get; set; }

    public string? Genre { get; set; }

    public string? SongTitle { get; set; }

    public string? SongPath { get; set; }

    public string? M3uname { get; set; }

    public DateOnly? DateCreated { get; set; }
}
