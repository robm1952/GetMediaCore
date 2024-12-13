using GetMediaCore.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace GetMediaCore.Business
{
    internal class TagSharp
    {
        private readonly Data.DataLayer _dataLayer;
        private Artist? _artist;
        private Album? _album;
        private readonly Song _song;
        private readonly SongFile _songFile;
        private ArtistAlbumSongXref? _xref;
        private static List<Artist>? _artists;
        private int _artistCount = 0;
        private readonly List<Album> _albums;
        private readonly List<Genre> _genres;

        public TagSharp()
        {
            _dataLayer = new Data.DataLayer();
            _song = new Song();
            _songFile = new SongFile();
            _artists = [];
            _albums = [];
            _genres = GetGenres();
        }

        private Artist CreateArtisFromTagLib(TagLib.File tFile)
        {
            _artist = new Artist();
            _artists = _dataLayer.GetArtistsList();

            if (tFile.Tag.Performers.Length > 0)
            {
                foreach (var item in tFile.Tag.Performers)
                {
                    _artist.ArtistName += item;
                    if (item == "AC")
                    {
                        _artist.ArtistName += "/";
                    }
                    _artist.ArtistDisplayName = CheckAndUseSortName(_artist.ArtistName);
                }

                if (_artists.Count > 0 && _artists.Any(x => x.ArtistName == _artist.ArtistName))
                {
                    _artist.ArtistId = _artists.SingleOrDefault(x => x.ArtistName == _artist.ArtistName).ArtistId;
                }
            }
            else
            {
                System.Diagnostics.Trace.WriteLine($"{_artist.ArtistName} Tag Name = {tFile.Tag.Performers[0]}");
            }

            return _artist;
        }

        private Album CreatetAlbumFromTagLib(int artistId, TagLib.File tFile)
        {
            _album = new()
            {
                AlbumArtistId = artistId,
                AlbumTitle = tFile.Tag.Album,
                AlbumGenre = GetGenresId(tFile.Tag.Genres.FirstOrDefault()),
                AlbumYear = (Int32?)tFile.Tag.Year,
                AlbumDisc = (Int32?)tFile.Tag.Disc
            };
            if (_albums.Count > 0 && _albums.Any(x => x.AlbumTitle == _album.AlbumTitle))
            {
                _album.AlbumId = _albums.SingleOrDefault(x => x.AlbumTitle == _album.AlbumTitle).AlbumId;
            }

            return _album;
        }

        private static String? CheckAndUseSortName(String artistName)
        {
            return CheckForTheAtStartOfArtistName(artistName);
        }

        private static String? CheckForTheAtStartOfArtistName(String artistName)
        {
            Regex rgx = new(@"\AThe\b", RegexOptions.Compiled);
            Match match = rgx.Match(artistName);
            return match.Success ? CreateSortName(artistName) : artistName;
        }

        private static String CreateSortName(String artistName)
        {
            StringBuilder sb = new();
            string[] theSplits = artistName.Split([' ']);
            if (theSplits.Length > 1)
            {
                for (int x = 1; x < theSplits.Length; x++)
                {
                    sb.Append(theSplits[x]);
                }
                sb.Append(", " + theSplits[0]);
            }
            else
            {
                sb.Append(artistName);
            }

            return sb.ToString().Trim();
        }

        private Int32? GetGenresId(String genreName) => _genres.FirstOrDefault(x => x.GenreName.Equals(genreName, StringComparison.OrdinalIgnoreCase)).GenreId;

        private List<Genre> GetGenres()
        {
            return _dataLayer.GetGenres();
        }

        private Song CreateSongFromTagLib(int albumId, TagLib.File tFile)
        {
            _song.SongId = 0;
            _song.SongAlbumId = albumId;
            _song.SongTitle = tFile.Tag.Title;
            _song.SongDuration = tFile.Properties.Duration.Ticks;
            _song.SongTrackNumber = (Int32)tFile.Tag.Track;
            return _song;
        }

        private SongFile CreateSongFileFromTagLib(int songId, TagLib.File tFile)
        {
            _songFile.SongFileId = 0;
            _songFile.SongFileSongId = songId;
            _songFile.SongFileFullyQualifiedName = tFile.FileAbstraction.Name;
            _songFile.SongFileSize = (long)tFile.InvariantEndPosition;
            if (_songFile.SongFileFullyQualifiedName.EndsWith(".mp3"))
            {
                _songFile.SongFileType = true;
            }
            else
            {
                _songFile.SongFileType = false;
            }
            return _songFile;
        }

        internal void ParseListWithTagLib(List<FileSystemInfo> fsList)
        {
            foreach (FileSystemInfo fs in fsList)
            {
                var tFile = TagLib.File.Create(fs.FullName);

                if (tFile != null)
                {
                    Artist artist = CreateArtisFromTagLib(tFile);
                    if (artist.ArtistId == 0)
                    {
                        _artists.Add(artist);
                        _dataLayer.PutEntityToDb(artist);
                    }

                    Album album = CreatetAlbumFromTagLib(artist.ArtistId, tFile);
                    if (album.AlbumId == 0)
                    {
                        _albums.Add(album);
                        _dataLayer.PutEntityToDb(album);
                    }

                    if (!_dataLayer.CheckForExistingSongPath(fs.FullName))
                    {
                        Song song = CreateSongFromTagLib(album.AlbumId, tFile);

                        _dataLayer.PutEntityToDb(song);

                        _xref = new ArtistAlbumSongXref(artist.ArtistId, album.AlbumId, song.SongId)
                        {
                            RefId = 0,
                            ArtistId = artist.ArtistId,
                            AlbumId = album.AlbumId,
                            SongId = song.SongId
                        };
                        _dataLayer.PutEntityToDb(_xref);

                        SongFile songFile = CreateSongFileFromTagLib(song.SongId, tFile);

                        _dataLayer.PutEntityToDb(songFile);
                    }
                }
            }
        }
    }
}
