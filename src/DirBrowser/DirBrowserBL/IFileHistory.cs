namespace DirBrowserBL;

public interface IFileHistory
{
    public long DBId { get; set; }
    public string? User { get; set; }
    public string? Content { get; set; }
    public string? FileName { get; set; }
    public string KeyHistory();
    public DateTimeOffset LastModified { get; }

}
