using GetMediaCore.Data;
using GetMediaCore.Models;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace GetMediaCore.Traffic {
    internal class TagSharp {
        private IConfiguration _configuration;
        private InfoLayer _il;

        public TagSharp(IConfiguration configuration) {
            _configuration = configuration;
            _il = new InfoLayer(_configuration);
        }

        public void ProcessListToDb(List<FileInfo> lfi) {
            foreach (FileInfo fi in lfi) {
                try {
                    var tFile = TagLib.File.Create(fi.FullName);
                    if (tFile != null) {
                        // check here to ensure genre, album, artist are not empty strings
                        Artist artist = AddArtist(tFile);
                        if (artist != null) {
                            Album album = AddAlbum(tFile, artist.ArtistId);
                            if (album != null) {
                                Song song = AddSong(tFile, album.AlbumId);
                                if (song != null) {
                                    ArtistAlbumSongXref xref = new ArtistAlbumSongXref() {
                                        ArtistId = artist.ArtistId,
                                        AlbumId = album.AlbumId,
                                        SongId = song.SongId
                                    };
                                    _il.PutEntityToDb(xref);
                                    SongFile songFile = AddSongFile(tFile, song.SongId, fi);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error processing file '{fi.FullName}': {ex}");
                }
            }
        }

        private Song AddSong(TagLib.File tFile, int albumId) {
            Song song = new Song() {
                SongAlbumId = albumId,
                SongDuration = tFile.Properties.Duration.Ticks,
                SongTitle = tFile.Tag.Title,
                SongTrackNumber = (int)tFile.Tag.Track,
            };
            song = _il.PutEntityToDb(song);
            return song;
        }

        private SongFile AddSongFile(TagLib.File tfile, int songId, FileInfo fi) {

            SongFile songFile = new SongFile() {
                SongFileSongId = songId,
                SongFileFqn = fi.FullName,
                SongFileSize = fi.Length,
                SongFileType = true,
            };
            songFile = _il.PutEntityToDb(songFile);
            return songFile;
        }

        private Album AddAlbum(TagLib.File tFile, int artistId) {
            //need to check the genre value in the tFile for empty genre
            //what to do with these --salt genres with unknown 
            Album album = new Album() {
                AlbumArtistId = artistId,
                AlbumTitle = tFile.Tag.Album,
                AlbumYear = (Int16?)tFile.Tag.Year,
                AlbumDisc = (Int16?)tFile.Tag.Disc,
                AlbumGenre = GetGenreId(tFile.Tag.Genres ?? System.Array.Empty<string>()),
            };

            album = _il.CheckDB(album);
            //if (album != null)
            if (album.AlbumId == 0) {
                album = _il.PutEntityToDb(album);
            }
            return album;
        }

        private int? GetGenreId(string[] genres) {
            try {
                if (genres == null || genres.Length == 0) {
                    return 0;
                }

                foreach (string genre in genres) {
                    if (string.IsNullOrWhiteSpace(genre))
                        continue;

                    var g = genre.Trim();
                    var car = _il.CheckDB(g);
                    if (car != null) {
                        return car.GenreId;
                    }

                    car = AddGenre(g);
                    if (car != null) {
                        return car.GenreId;
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        private Genre? AddGenre(string GenreName) {
            Genre genre = new Genre() {
                GenreName = GenreName
            };
            return _il.PutEntityToDb(genre);
        }

        private Artist AddArtist(TagLib.File tFile) {
            Artist artist = new Artist();
            var performers = tFile.Tag.Performers;
            if (performers != null && performers.Length > 0) {
                artist.ArtistName = string.Join(", ", performers.Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => p.Trim()));
            }
            else {
                artist.ArtistName = "Artist Missing";
            }

            artist.ArtistSortName = CreateSortName(artist.ArtistName);
            artist = _il.CheckDB(artist);
            if (artist.ArtistId == 0) {
                return _il.PutEntityToDb(artist);
            }
            else {
                return artist;
            }
        }

        private string? CreateSortName(string? artistName) {
            if (string.IsNullOrWhiteSpace(artistName))
                return artistName;

            var name = artistName.Trim();
            // match leading 'the ' case-insensitive
            var regex = new Regex("^the\\s+", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            var result = regex.Replace(name, string.Empty);
            if (!string.Equals(result, name, StringComparison.Ordinal)) {
                return $"{result}, The";
            }

            return name;
        }
    }
}
