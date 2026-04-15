using GetMediaCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace GetMediaCore.Data
{
    internal class InfoLayer
    {
        private IConfiguration _configuration;
        private MediaCore _mediaCore;
        private readonly ILogger<InfoLayer> _logger;

        public InfoLayer(IConfiguration configuration, ILogger<InfoLayer>? logger = null)
        {
            _configuration = configuration;
            _mediaCore = new MediaCore();
            _logger = logger ?? NullLogger<InfoLayer>.Instance;
        }

        public Artist PutEntityToDb(Artist artist)
        {
            if (artist == null) return artist;
            try
            {
                using (var ctx = new MediaCore())
                {
                    ctx.Add(artist);
                    ctx.SaveChanges();
                    // Return the tracked/saved entity (ensure id populated)
                    var saved = ctx.Artists.Find(artist.ArtistId);
                    return saved ?? artist;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Artist to database");
                return artist;
            }
        }

        public Album PutEntityToDb(Album album)
        {
            try
            {
                using (_mediaCore = new MediaCore())
                {
                    _mediaCore.Add(album);
                    _mediaCore.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return album;
        }

        public Genre? PutEntityToDb(Genre genre)
        {
            try
            {
                using (_mediaCore = new MediaCore())
                {
                    _mediaCore.Add(genre);
                    _mediaCore.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Genre to database");
                return null;
            }
            return genre;
        }

        public Song PutEntityToDb(Song song)
        {
            if (song != null)
            {
                try
                {
                    using (_mediaCore = new MediaCore())
                    {
                        _mediaCore.Add(song);
                        _mediaCore.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving Song to database");
                    return new Song();
                }
                return song;
            }
            // Ensure a non-null Song is always returned to fix CS8603
            return new Song();
        }

        public SongFile PutEntityToDb(SongFile songFile)
        {
            if (songFile != null)
            {
                try
                {
                    using (_mediaCore = new MediaCore())
                    {
                        _mediaCore.Add(songFile);
                        _mediaCore.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving SongFile to database");
                    return new SongFile();
                }
                return songFile;
            }
            return new SongFile();
        }

        public ArtistAlbumSongXref PutEntityToDb(ArtistAlbumSongXref xref)
        {
            if (xref != null)
            {
                try
                {
                    using (_mediaCore = new MediaCore())
                    {
                        _mediaCore.Add(xref);
                        _mediaCore.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving ArtistAlbumSongXref to database");
                    return new ArtistAlbumSongXref();
                }
                return xref;
            }
            return new ArtistAlbumSongXref();
        }

        public List<string> GetSongFileFQNs()
        {
            // Filter out null SongFileFqn values to avoid CS8604
            using (var ctx = new MediaCore())
            {
                return ctx.SongFiles
                    .Where(x => x.SongFileFqn != null)
                    .Select(x => x.SongFileFqn!)
                    .ToList();
            }
        }

        internal Artist CheckDB(Artist artist) {
            if (artist == null) return new Artist();
            var name = artist.ArtistName?.Trim() ?? string.Empty;
            using (var ctx = new MediaCore())
            {
                var existing = ctx.Artists
                    .Where(x => x.ArtistName != null && x.ArtistName.Trim().Equals(name, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();
                return existing ?? artist;
            }
        }

        internal Album CheckDB(Album album)
        {
            using (_mediaCore = new MediaCore())
            {
                if (_mediaCore.Albums.Any(x => x.AlbumTitle == album.AlbumTitle))
                {
                    // Fix CS8603: Ensure non-null return by using null-coalescing operator
                    return _mediaCore.Albums.Where(x => x.AlbumTitle == album.AlbumTitle).FirstOrDefault() ?? album;
                }
                else
                {
                    return album;
                }
            }
        }

        internal Genre? CheckDB(string Genre)
        {

            using (_mediaCore = new MediaCore())
            {
                if (_mediaCore.Genres.Any(x => x.GenreName == Genre))
                {
                    return _mediaCore.Genres.Where(x => x.GenreName == Genre).FirstOrDefault();
                }
            }
            return null;
        }

    }
}
