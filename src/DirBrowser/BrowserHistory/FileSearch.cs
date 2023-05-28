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
    public async Task<IFileInfo[]> SearchFiles(string contentLine, string startFolder)
    {
         
        //do not move sql
        // or sqlinjection will appear
        var data = 
            await context.ModifiedUserFile.FromSqlInterpolated($@"
select muf.* from 
(SELECT max(id) as id
  FROM [ModifiedUserFile]
  group by IDFile 
)  a 
  inner join [ModifiedUserFile] muf on muf.id = a.id
  [ModifiedFile] mf on mf.IDFile = muf.IDFile
  where 
mf.FullPathFile like '{startFolder}%' and
muf.Contents like '%{contentLine}%'
    
")
            .Include(muf=>muf.IDFileNavigation)
            .AsNoTracking()
            .ToArrayAsync();
        
                
        var lst=new List<IFileInfo>();
        foreach (var item in data)
        {
            var pathFile = item.IDFileNavigation.FullPathFile;
            if (File.Exists(pathFile))
            {
                var file = new FileInfo(pathFile);
                ArgumentNullException.ThrowIfNull(file);
                var fld = new FolderToRead(file, startFolder);
                lst.Add(fld);
            }

        }
        return lst.ToArray();
    }
}
