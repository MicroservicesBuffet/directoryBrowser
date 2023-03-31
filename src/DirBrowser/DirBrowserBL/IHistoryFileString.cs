namespace DirBrowserBL;

public interface IHistoryFileString
{
    Task<long> AddHistory(IFileHistory fileHistory);
    Task<IFileHistory[]?> History(IFileInfo fld);
}
