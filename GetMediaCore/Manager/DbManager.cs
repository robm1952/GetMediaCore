using GetMediaCore.Data;
using GetMediaCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace GetMediaCore.Manager {
    /// <summary>
    /// DbManager controls access to the infoLayer. So a manager instiates an instance to get to the database
    /// </summary>
    public class DbManager {
        private readonly IConfiguration _config;
        private InfoLayer _infoLayer;
        private readonly ILogger<DbManager> _logger;
        public DbManager(IConfiguration configuration, ILogger<DbManager>? logger = null) {
            _config = configuration;
            _logger = logger ?? NullLogger<DbManager>.Instance;
            _infoLayer = new InfoLayer(configuration);
        }

        public HashSet<FileInfo> GetSongFileInfo() {
            HashSet<FileInfo> list = new HashSet<FileInfo>();
            HashSet<string> LocalFQNs = _infoLayer.GetSongFileFQNs().ToHashSet<string>();

            foreach (string fqn in LocalFQNs) {
                FileInfo fileInfo = new FileInfo(fqn);
                list.Add(fileInfo);
            }
            return list;
        }

        internal long GetSongFileCount() {
            return _infoLayer.GetSongFileFQNs().Count;
        }

        internal void RemoveSongsFromDatabase(List<FileInfo> fileInfos2Delete) {
            if (fileInfos2Delete == null || fileInfos2Delete.Count == 0)
                return;

            try {
                var paths = fileInfos2Delete.Select(f => Path.GetFullPath(f.FullName)).ToList();

                using (var db = new MediaCore()) {
                    // Find SongFile records that match the provided paths
                    var songFiles = db.SongFiles
                        .Where(sf => sf.SongFileFqn != null && paths.Contains(sf.SongFileFqn))
                        .ToList();

                    if (songFiles.Count == 0)
                        return;

                    // Collect affected songIds
                    var songIds = songFiles.Select(sf => sf.SongFileSongId).Distinct().ToList();

                    // Remove the SongFile records
                    db.SongFiles.RemoveRange(songFiles);
                    db.SaveChanges();

                    // For each songId, if no SongFile remains, remove xrefs and the Song
                    foreach (var songId in songIds) {
                        bool hasRemainingFiles = db.SongFiles.Any(sf => sf.SongFileSongId == songId);
                        if (!hasRemainingFiles) {
                            // Remove xrefs
                            var xrefs = db.ArtistAlbumSongXrefs.Where(x => x.SongId == songId).ToList();
                            if (xrefs.Count > 0) {
                                db.ArtistAlbumSongXrefs.RemoveRange(xrefs);
                            }

                            // Remove the Song
                            var song = db.Songs.Where(s => s.SongId == songId).FirstOrDefault();
                            if (song != null) {
                                db.Songs.Remove(song);
                            }
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error removing songs from database");
            }
        }
    }

}
