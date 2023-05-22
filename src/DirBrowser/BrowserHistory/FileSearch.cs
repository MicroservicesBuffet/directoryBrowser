using DirBrowserBL;
using Generated;
using Microsoft.Extensions.FileProviders;
using System.Linq.Expressions;

namespace BrowserHistory;

public class FileSearch : IFileSearch
{
    private readonly ISearchDataModifiedFile context;
    private readonly FolderToRead[] folders;

    public FileSearch(ISearchDataModifiedFile context, FolderToRead[] folders)
    {
        this.context = context;
        this.folders = folders;
    }
    public async Task<IFileInfo[]> SearchFiles(string startFolder, string nameFile)
    {
        
        var lst=new List<IFileInfo>();
        await foreach(var item in context.ModifiedFileSimpleSearch_FullPathFile(GeneratorFromDB.SearchCriteria.StartsWith, startFolder))
        {
            if (item.FullPathFile.EndsWith(nameFile, StringComparison.InvariantCultureIgnoreCase))
            {
                if (File.Exists(item.FullPathFile))
                {
                    
                    var file = new FileInfo(item.FullPathFile);
                    ArgumentNullException.ThrowIfNull(file);
                    var fld = new FolderToRead(file,startFolder);
                    lst.Add(fld);
                }

            }
        }
        return lst.ToArray();
    }
}
