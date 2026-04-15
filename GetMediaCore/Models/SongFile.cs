using System;
using System.Collections.Generic;

namespace GetMediaCore.Models;

public partial class SongFile
{
    public int SongFileId { get; set; }

    public int SongFileSongId { get; set; }

    public string? SongFileFqn { get; set; }

    public long? SongFileSize { get; set; }

    public bool? SongFileType { get; set; }
}
