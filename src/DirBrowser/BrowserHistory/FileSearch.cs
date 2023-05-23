using Microsoft.EntityFrameworkCore;

namespace BrowserHistory;

public class FileSearch : IFileSearch
{
    private readonly ApplicationDBContext context;
    private readonly FolderToRead[] folders;

    public FileSearch(ApplicationDBContext context, FolderToRead[] folders)
    {
        this.context = context;
        this.folders = folders;
    }
    public async Task<IFileInfo[]> SearchFiles(string startFolder, string contentLine)
    {
         

        var data = 
            context.Database.SqlQuery<ModifiedUserFile_Table>($@"select muf.* from 
(SELECT max(id) as id
  FROM [ModifiedUserFile]
  group by IDFile 
)  a 
  inner join [ModifiedUserFile] muf on muf.id = a.id
  where muf.Contents like '%{contentLine}%'
").ToArrayAsync();
        
                
        var lst=new List<IFileInfo>();
        foreach(var item in data)
        {
            var pathFile = item.IDFileNavigation.FullPathFile;
            if (pathFile.StartsWith(contentLine, StringComparison.InvariantCultureIgnoreCase))
            {
                if (File.Exists(pathFile))
                {                    
                    var file = new FileInfo(pathFile);
                    ArgumentNullException.ThrowIfNull(file);
                    var fld = new FolderToRead(file,startFolder);
                    lst.Add(fld);
                }

            }
        }
        return lst.ToArray();
    }
}
