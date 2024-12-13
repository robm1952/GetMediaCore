using GetMediaCore.Data;

namespace GetMediaCore.Business
{
    internal class MCAuditor
    {
        private static List<FileSystemInfo>? _FileSystemInfos;
        private static List<string> _FullyQualifiedNames;
        private static List<FileSystemInfo>? _AddToDb;
        private static List<string>? _RemoveFromDb;
        private static FSGetter? _FSGetter;
        private static DataLayer? _DataLayer;
        public MCAuditor()
        {
            _FSGetter = new();
            _DataLayer = new();
            _FileSystemInfos = [];
            _FullyQualifiedNames = [];
            _AddToDb = [];
            _RemoveFromDb = [];
        }

        public static void FillLists()
        {
            _FileSystemInfos = _FSGetter.ReadDataFromFileSystem();
            _FullyQualifiedNames = _DataLayer.FillFQNList();
        }

        internal List<string> CompareDBtoFS()
        {
            foreach (string s in _FullyQualifiedNames)
            {
                if (File.Exists(s)) { continue; } else { _RemoveFromDb.Add(s); }
            }
            return _RemoveFromDb;
        }

        internal static List<FileSystemInfo> CompareFStoDB()
        {
            foreach (FileSystemInfo fsi in _FileSystemInfos)
            {
                if (_DataLayer.CheckForExistingSongPath(fsi.FullName))
                { continue; }
                else { _AddToDb.Add(fsi); }
            }
            return _AddToDb;
        }

        internal static void RemoveFromDataBase(List<string> ListFQN)
        {
            foreach (string s in ListFQN)
            {
                _DataLayer.RemoveFromDatabase(s);
            }
        }
    }
}
