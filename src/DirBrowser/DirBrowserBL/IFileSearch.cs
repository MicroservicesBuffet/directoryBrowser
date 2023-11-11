
namespace DirBrowserBL;

public interface IFileSearch
{
    Task<FolderToRead[]> SearchFiles(string startFolder,string contentLine, FolderToRead[] rootFolders); 

}


