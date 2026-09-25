using GetMediaCore.Manager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

internal class Program {
    private static IConfiguration? _config;
    private static FileManager? _fileManager;
    private static DbManager? _dbManager;
    private static HashSet<FileInfo>? lfi;
    private static TrafficManager? tm;
    private static long dbCount = 0;

    private static void Main(string[] args) {
        _config = InitializeConfig();

        // wire up logging
        using var loggerFactory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Information));
        ILogger logger = loggerFactory.CreateLogger<Program>();
        logger.LogInformation("Application started.");

        Managers _managers = new Managers(_config, loggerFactory);

        _dbManager = _managers.GetDBManager();
        _fileManager = _managers.GetFileManager();
        tm = _managers.GetTrafficManager();

        lfi = [.. _fileManager.GetFiles()];

        ResolveFileSystemDb(_fileManager, tm);
    }

    private static void ResolveFileSystemDb(FileManager _fileManager, TrafficManager tm) {
        if (_dbManager == null) {
            throw new InvalidOperationException("DbManager is not initialized.");
        }

        dbCount = _dbManager.GetSongFileCount();
        if (dbCount == 0) {
            var newFiles = _fileManager.GetFiles();
            tm.ProcessFiles(newFiles);
            return;
        }

        HashSet<FileInfo> lSongFQN = _dbManager.GetSongFileInfo().ToHashSet<FileInfo>();
        HashSet<FileInfo> fsFiles = _fileManager.GetFiles().ToHashSet<FileInfo>();

        // Use HashSet of full paths for efficient and correct comparisons
        var fsSet = new HashSet<string>(fsFiles.Select(f => Path.GetFullPath(f.FullName)), StringComparer.OrdinalIgnoreCase);
        var dbSet = new HashSet<string>(lSongFQN.Select(f => Path.GetFullPath(f.FullName)), StringComparer.OrdinalIgnoreCase);

        // Files that are in filesystem but not in DB -> add
        var toAddPaths = fsSet.Except(dbSet).ToList();
        if (toAddPaths.Count > 0) {
            var toAdd = toAddPaths.Select(p => new FileInfo(p)).ToList();
            tm.ProcessFiles(toAdd);
        }

        // Files that are in DB but not on filesystem -> delete
        var toDeletePaths = dbSet.Except(fsSet).ToList();
        if (toDeletePaths.Count > 0) {
            var toDelete = toDeletePaths.Select(p => new FileInfo(p)).ToList();
            _dbManager.RemoveSongsFromDatabase(toDelete);
        }
    }

    private static IConfiguration InitializeConfig() {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
        IConfiguration configuration = builder.Build();
        return configuration;
    }
}