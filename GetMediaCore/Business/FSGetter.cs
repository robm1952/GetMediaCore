namespace GetMediaCore.Business
{
    internal class FSGetter
    {

        private string MusicPath { get; set; }
        private string Mp3 { get; set; } = PageStrings.MP3;
        private string Flac { get; set; } = PageStrings.Flac;
        public FSGetter()
        {
            MusicPath = PageStrings.Musicpath;

        }

        public List<FileSystemInfo> ReadDataFromFileSystem()
        {
            List<FileSystemInfo> fList;
            DirectoryInfo di = new(MusicPath);

            fList = [.. di.GetFileSystemInfos(Mp3, SearchOption.AllDirectories)];
            fList.AddRange([.. new DirectoryInfo(MusicPath).GetFileSystemInfos(Flac, SearchOption.AllDirectories)]);

            return fList;
        }
    }

    public static partial class PageStrings
    {
        public static string Musicpath { get; set; } = @"D:\DMusic";
        public static string MP3 { get; set; } = "*.mp3";
        public static string Flac { get; set; } = "*.flac";
    }
}

