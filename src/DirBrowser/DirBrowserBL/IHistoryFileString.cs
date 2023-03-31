namespace DirBrowserBL;

public interface IHistoryFileString
{
    Task<long> AddHistory(IFileHistory fileHistory);
    Task<IFileHistory?> GetFileContents(long id);
    Task<IFileHistory[]?> History(IFileInfo fld);
}
