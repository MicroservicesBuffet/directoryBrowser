
namespace DirBrowserBL;

public interface IFileSearch
{
    Task<IFileInfo[]> SearchFiles(string startFolder,string nameFile); 

}


