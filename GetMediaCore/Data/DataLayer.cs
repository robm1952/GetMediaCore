using GetMediaCore.Models;


namespace GetMediaCore.Data
{
    internal class DataLayer
    {
        public MediaContext _dbContext;
        public DataLayer()
        {

        }

        internal List<Artist> GetArtistsList()
        {
            using (_dbContext = new MediaContext())
            {
                return _dbContext.Artists.ToList();
            }
        }
        internal void PutEntityToDb(Artist artist)
        {
            using (_dbContext = new MediaContext())
            {
                _dbContext.Add(artist);
                _dbContext.SaveChanges();
            }
        }
        internal void PutEntityToDb(Album album)
        {
            using (_dbContext = new MediaContext())
            {
                _dbContext.Add(album);
                _dbContext.SaveChanges();
            }
        }

        internal void PutEntityToDb(ArtistAlbumSongXref xref)
        {
            using (_dbContext = new MediaContext())
            {
                _dbContext.ArtistAlbumSongXrefs.Add(xref);
                _dbContext.SaveChanges();
            }
        }

        internal void PutEntityToDb(Song song)
        {
            using (_dbContext = new MediaContext())
            {
                _dbContext.Add(song);
                _dbContext.SaveChanges();
            }
        }
        internal void PutEntityToDb(SongFile songFile)
        {
            using (_dbContext = new MediaContext())
            {
                _dbContext.Add(songFile);
                _dbContext.SaveChanges();
            }
        }

        internal bool CheckForAny()
        {
            using (_dbContext = new MediaContext())
            {
                return _dbContext.Artists.Any(x => x.ArtistId > 0);
            }
        }

        internal int GetCount()
        {
            return _dbContext.Artists.Count();
        }

        internal List<Genre> GetGenres()
        {
            List<Genre> genresFromDB = [];
            List<Genre> genreToApp = [];

            using (_dbContext = new MediaContext())
            {
                genresFromDB = [.. _dbContext.Genres];
            }

            foreach (var genre in genresFromDB)
            {
                genreToApp.Add(new Genre(genre.GenreId, genre.GenreName.ToLowerInvariant()));
            }

            return genreToApp;
        }

        internal Boolean CheckForExistingSongPath(String fullName)
        {
            using (_dbContext = new MediaContext())
            {
                return _dbContext.SongFiles.Any(x => x.SongFileFullyQualifiedName == fullName);
            }
        }

        internal List<string> FillFQNList()
        {
            List<SongFile> LSF = [];
            List<string> FQN = [];// new List<string>();
            using (_dbContext = new MediaContext())
            {
                LSF = [.. _dbContext.SongFiles];
            }

            foreach (SongFile songFile in LSF)
            {
                FQN.Add(songFile.SongFileFullyQualifiedName);
            }
            return FQN;
        }

        internal void RemoveFromDatabase(string FQN)
        {
            int SongId = RemoveSongFile(FQN);
            int AlbumId = RemoveSong(SongId);
            int RefId = RemoveSongFromXref(SongId);
            int ArtistId = RemoveAlbum(AlbumId);
            RemoveArtist(ArtistId);
        }

        private Int32 RemoveSongFromXref(Int32 songId)
        {
            Int32 xRefId = 0;
            using (_dbContext = new MediaContext())
            {
                if (_dbContext.ArtistAlbumSongXrefs.Any(x => x.SongId == songId))
                {
                    ArtistAlbumSongXref xref = _dbContext.ArtistAlbumSongXrefs.FirstOrDefault(x => x.SongId == songId);
                    xRefId = xref.RefId;
                    if (xref != null)
                    {
                        _dbContext.ArtistAlbumSongXrefs.Remove(xref);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return xRefId;
        }

        private void RemoveArtist(int ArtistId)
        {
            using (_dbContext = new MediaContext())
            {
                if (ArtistId == 0)
                {
                    return;
                }
                else if (!_dbContext.Albums.Any(x => x.AlbumArtistId == ArtistId))
                {
                    Artist artist = _dbContext.Artists.FirstOrDefault(x => x.ArtistId == ArtistId);
                    _dbContext.Artists.Remove(artist);
                    _dbContext.SaveChanges();
                }
            }
        }

        private int RemoveAlbum(int AlbumId)
        {
            int ArtistId = 0;
            if (AlbumId > 0)
            {
                using (_dbContext = new MediaContext())
                {
                    if (AlbumId == 0)
                    {
                        ArtistId = 0;
                    }
                    else if (!_dbContext.Songs.Any(x => x.SongAlbumId == AlbumId))
                    {
                        Album album = _dbContext.Albums.FirstOrDefault(x => x.AlbumId == AlbumId);
                        ArtistId = album.AlbumArtistId;
                        _dbContext.Albums.Remove(album);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return ArtistId;
        }

        private int RemoveSong(int SongId)
        {
            int AlbumId = 0;
            using (_dbContext = new MediaContext())
            {
                Song song = _dbContext.Songs.FirstOrDefault(x => x.SongId == SongId);
                AlbumId = song.SongAlbumId;
                _dbContext.Songs.Remove(song);
                _dbContext.SaveChanges();
                if (_dbContext.Songs.Any(x => x.SongAlbumId == AlbumId))
                {
                    AlbumId = 0;
                }
            }

            return AlbumId;
        }

        private int RemoveSongFile(String fQN)
        {
            int SongId = -1;
            using (_dbContext = new MediaContext())
            {
                SongFile removeSF = _dbContext.SongFiles.FirstOrDefault(x => x.SongFileFullyQualifiedName == fQN);
                SongId = removeSF.SongFileSongId;
                _dbContext.SongFiles.Remove(removeSF);
                _dbContext.SaveChanges();
            }
            return SongId;
        }

        internal Int32 GetArtistsCount()
        {
            return _dbContext.Artists.Count();
        }
    }
}
