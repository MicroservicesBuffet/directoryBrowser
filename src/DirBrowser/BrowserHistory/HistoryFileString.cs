using DirBrowserBL;

namespace BrowserHistory;

public class HistoryFileString : IHistoryFileString
{
    static Dictionary<string, IFileHistory> history = new();
    public int AddHistory(IFileHistory fileHistory)
    {
        history.Add(fileHistory.KeyHistory(), fileHistory);
        return history.Count;
    }
    public IFileHistory[] History(FolderToRead fld)
    {
        return history
            .Where(it=>it.Key.StartsWith(fld.PhysicalPath))
            .Select(it=>it.Value)
            .ToArray();
    }
}