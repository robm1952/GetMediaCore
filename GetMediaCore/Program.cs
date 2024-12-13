using GetMediaCore.Business;
using GetMediaCore.Data;

internal class Program
{
    private static readonly TagSharp ts = new();
    private static DataLayer? _dataLayer;
    private static MCAuditor? _mcauditor;
    private static List<FileSystemInfo>? _addToDb;
    private static List<string>? _removeFromDb;
    private static void Main()
    {
        _dataLayer = new DataLayer();
        _mcauditor = new MCAuditor();

        if (_dataLayer.CheckForAny())
        {
            MCAuditor.FillLists();
            _addToDb = MCAuditor.CompareFStoDB();
            if (_addToDb.Count > 0)
            {
                ts.ParseListWithTagLib(_addToDb);
            }
            _removeFromDb = _mcauditor.CompareDBtoFS();
            if (_removeFromDb.Count > 0)
            {
                MCAuditor.RemoveFromDataBase(_removeFromDb);
            }
        }
        else
        {
            FSGetter musicRawData = new();

            List<FileSystemInfo> MRDList = musicRawData.ReadDataFromFileSystem();

            ts.ParseListWithTagLib(MRDList);
        }
    }
}

