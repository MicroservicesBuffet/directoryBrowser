namespace DirBrowserBL
{
    public interface IFileOperations
    {
        string FullPathFile(string path, FolderToRead[] folders);
        Task<IFileHistory?> GetFileContents(long id);
        Task<IFileHistory[]?> GetFileHistory(string path, FolderToRead[] folders);
        Task<string> GetFileText(string path, FolderToRead[] folders);
        FolderToRead[] GetFolderContent(string path, FolderToRead[] folders);
        bool IsFolder(string path, FolderToRead[] folders);
        Task<int> SetFileText(string user, SaveTextFile save, FolderToRead[] folders, params ISaveFile[] plugins);
    }
}