using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirBrowserBL;
public class SaveTextFile
{
    public string? pathFile { get; set; }
    public string? content { get; set; }
}


public class FileOperations
{
    private readonly IHistoryFileString historyFileString;

    public FileOperations(IHistoryFileString historyFileString)
    {
        this.historyFileString = historyFileString;
    }
    public IFileHistory[] GetFileHistory(string path, FolderToRead[] folders)
    {

        var file = FullPathFile(path, folders);
        var fld = new FolderToRead(new FileInfo(file));
        return historyFileString.History(fld);
    }
    public async Task<string> GetFileText(string path,  FolderToRead[] folders)
    {
        
        var file = FullPathFile(path, folders);
        return await System.IO.File.ReadAllTextAsync(file);
    }
    public async Task<int> SetFileText(string user, SaveTextFile save, FolderToRead[] folders)
    {
        
        var file = FullPathFile(save.pathFile ?? "", folders);
        await System.IO.File.WriteAllTextAsync(file, save.content ?? "");
        var fld = new FolderToRead(new FileInfo(file));
        IFileHistory fileHistory = fld;
        fileHistory.Content = save.content;
        fileHistory.User = user;
        historyFileString.AddHistory(fileHistory);
        return (save.content ?? "").Length;
    }
    

    public string FullPathFile(string path,  FolderToRead[] folders)
    {
        var f = GetFirstFolder(path, folders);
        var str = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
        var pathFull = f.TransformFullPath;
        for (int i = 1; i < str.Length - 1; i++)
        {
            var path1 = str[i];
            pathFull = Path.Combine(pathFull, path1);
            if (!Directory.Exists(pathFull))
                throw new DirectoryNotFoundException($"{pathFull} must exists");

        }
        var file = Path.Combine(pathFull, str[str.Length - 1]);
        return file;
    }
    static FolderToRead GetFirstFolder(string path, FolderToRead[] folders)
    {
        if (path.Contains("%2f"))
            path = path.Replace("%2f", "/");
        if (path.Contains("%2F"))
            path = path.Replace("%2F", "/");

        var str = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
        if (str.Length == 0)
            throw new ArgumentException($"{nameof(path)} must have data");

        var f = folders.FirstOrDefault(it =>

        string.Equals(it.Id, str[0], StringComparison.InvariantCultureIgnoreCase));

        if (f == null)
        {
            var startF = string.Join("|", folders.Select(it => it.Id).ToArray());
            throw new ArgumentException($"{nameof(path)} = {path} must start with folders {startF}");
        }
        return f;
    }
    public bool IsFolder(string path, FolderToRead[] folders)
    {
        
        try
        {
            var di = FolderFromContent(path, folders);
            return di != null;
        }
        catch
        {
            return false;
        }
    }
    static DirectoryInfo FolderFromContent(string path, FolderToRead[] folders)
    {
        var f = GetFirstFolder(path, folders);
        var str = path.Split("/", StringSplitOptions.RemoveEmptyEntries);
        var pathFull = f.TransformFullPath;
        for (int i = 1; i < str.Length; i++)
        {
            var path1 = str[i];
            pathFull = Path.Combine(pathFull, path1);
            if (!Directory.Exists(pathFull))
                throw new DirectoryNotFoundException($"{pathFull} must exists");

        }
        var di = new DirectoryInfo(pathFull);
        return di;
    }
    public FolderToRead[] GetFolderContent(string path,  FolderToRead[] folders)
    {
        
        var di = FolderFromContent(path, folders);
        var files = di.EnumerateFiles().ToArray().Select(it => new FolderToRead(it)).ToArray();
        var dirs = di.EnumerateDirectories().ToArray().Select(it => new FolderToRead(it)).ToArray();

        List<FolderToRead> ret = new();
        if (dirs.Length > 0)
            ret.AddRange(dirs);
        if (files.Length > 0)
            ret.AddRange(files);
        return ret.ToArray();

    }
}
