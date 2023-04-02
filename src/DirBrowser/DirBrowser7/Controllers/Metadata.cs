using Asp.Versioning;
using Generated;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DirBrowser7.Controllers;


[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class Metadata : ControllerBase
{
    private readonly ApplicationDBContext context;

    public Metadata(ApplicationDBContext context)
    {
        this.context = context;
    }
    [HttpPost]
    public async Task<bool> CreateDb()
    {
        await context.Database.EnsureCreatedAsync();
        var u = new ModifiedUser();
        u.NameUser = "AutomaticStartFirstTimeUser";
        context.ModifiedUser.Add(u);
        await context.SaveChangesAsync();
        return true;
    }
    [HttpPost]
    public async Task<long> AddAllFilesToDB([FromServices] FileOperations fo ,[FromServices] FolderToRead[] fld)
    {
        long nr = 0;
        foreach (var item in fld)
        {
            nr+= await AddFolderRecursive(fo, item.Name,fld); 
        }
        return nr;
    }
    private async Task<long> AddFolderRecursive(FileOperations fo,string path, FolderToRead[] fld)
    {
        long nr = 0;
        foreach(var item in fo.GetFolderContent(path, fld))
        {
            if(item.IsDirectory)
            {
                //var name = fo.FullPathFile(item.FullPath, fld);
                nr +=await AddFolderRecursive(fo,item.RelPathFolder??"", fld);
                continue;
            }
            //is file
            
            ModifiedFile? file = context.ModifiedFile.FirstOrDefault(it => it.FullPathFile == item.TransformFullPath) ;  
            if(file == null){
                file = new ();
                file.FullPathFile = item.TransformFullPath;
                context.ModifiedFile.Add(file);
                await context.SaveChangesAsync();
            }

            var muf = context.ModifiedUserFile.FirstOrDefault(it => it.IDFile == file.IDFile);
            if (muf != null)
                continue;
            muf = new ModifiedUserFile();
            muf.IDFile = file.IDFile;
            muf.ModifiedDate = DateTime.Now;
            muf.Contents = await System.IO.File.ReadAllTextAsync(file.FullPathFile);
            muf.IDUser = 1;
            context.Add(muf);
            await context.SaveChangesAsync();
            nr++;   
        }
        return nr;
    }
}
