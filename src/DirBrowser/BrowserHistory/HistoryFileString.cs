using DirBrowserBL;
using Generated;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text;
using System.Text.Unicode;

namespace BrowserHistory;

public class HistoryFileString : IHistoryFileString
{
    public HistoryFileString(ApplicationDBContext context)
    {
        this.context = context;
    }
    //static Dictionary<string, IFileHistory> history = new();
    private readonly ApplicationDBContext context;

    public async Task<long> AddHistory(IFileHistory fileHistory)
    {
        //history.Add(fileHistory.KeyHistory(), fileHistory);
        var userName = fileHistory.User?.ToLower()??"no_user";
        var user =await context.ModifiedUser.FirstOrDefaultAsync(it => it.NameUser.ToLower() == userName);
        if (user == null)
        {
            user = new ModifiedUser()
            {
                NameUser = userName,
            };
            context.ModifiedUser.Add(user);
            await context.SaveChangesAsync();
        }
        var nameFile = fileHistory.FileName??"no_file";
        var file = await context.ModifiedFile.FirstOrDefaultAsync(it => it.FullPathFile == nameFile);
        if(file == null)
        {
            file = new ModifiedFile()
            {
                FullPathFile = nameFile,
            };
            context.ModifiedFile.Add(file);
            await context.SaveChangesAsync();
        }
        ModifiedUserFile muf = new()
        {
            Contents = fileHistory.Content?? Encoding.ASCII.GetBytes(" "),
            IDFile = file.IDFile,
            IDUser = user.IDUser,
            ModifiedDate = DateTime.UtcNow
        };
        context.ModifiedUserFile.Add(muf);
        await context.SaveChangesAsync();
        return await context.ModifiedUserFileCount(null);
    }
    public async Task<IFileHistory[]?> History(IFileInfo fld)
    {
        var file = await context.ModifiedFile.FirstOrDefaultAsync(it => it.FullPathFile == fld.PhysicalPath);
        if (file == null)
            return null;
        var history = context.ModifiedUserFile.Where(it => it.IDFile == file.IDFile)
            .Select(it => new
            {
                it.ModifiedDate,
                it.IDUserNavigation.NameUser
            })
            .ToArray();
        var ret = history.Select(it =>
        new FolderToRead()
        {
            FileName = file.FullPathFile,
            User = it.NameUser,
            LastModified = it.ModifiedDate
        }).Select(it=> it as IFileHistory).ToArray();
        
        return ret;

    }
}