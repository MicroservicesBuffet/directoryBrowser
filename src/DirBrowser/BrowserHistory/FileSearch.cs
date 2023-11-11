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
    public async Task<FolderToRead[]> SearchFiles(string startFolder, string contentLine, FolderToRead[] rootFolders)
    {
        await Task.Delay(5000); 

        var data = await
            context.Database.SqlQuery<long>($@"select muf.IDFile from 
(SELECT max(id) as id
  FROM [ModifiedUserFile]
  group by IDFile 
)  a 
  inner join [ModifiedUserFile] muf on muf.id = a.id
  where CHARINDEX({contentLine},muf.Contents) >0
").ToArrayAsync();
        if (data.Length == 0)
            return Array.Empty<FolderToRead>();

        var folderWhere = FileOperations.FolderFromContent(startFolder, rootFolders).FullName;
        //now find the files
        var idFiles =  data.Select(it=>it).Distinct().ToArray();
        
        //TODO: construct the method in the db generated
        var files = await context.ModifiedFile.Where(it=>idFiles.Contains(it.IDFile)).ToArrayAsync();
           
        var lst=new List<FolderToRead>();
        foreach (var item in files)
        {
            var pathFile = item.FullPathFile;
            if (!pathFile.StartsWith(folderWhere, StringComparison.InvariantCultureIgnoreCase))
                continue;

            if (File.Exists(pathFile))
            {

                var file = new FileInfo(pathFile);
                ArgumentNullException.ThrowIfNull(file);
                var newStartFolder = startFolder;
                var dir = file.Directory!.FullName;
                var dif = Path.GetRelativePath(folderWhere, dir);
                if (dif.Length > 1)
                {
                    newStartFolder = startFolder + "/" + dif.Replace(@"\", "/"); ;
                }
                var fld = new FolderToRead(file, newStartFolder);
                //fld.User = folderWhere;
                //fld.Content = newStartFolder;
                lst.Add(fld);
            }

        }
        
        return lst.Where(it=>!it.IsDirectory).ToArray();
    }
}
