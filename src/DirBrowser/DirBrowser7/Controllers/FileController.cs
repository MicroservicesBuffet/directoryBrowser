using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirBrowser7.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class FileController : ControllerBase
{
    [HttpGet("{*path}")]
    public async Task<string> GetFileLines(string path, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(5000);
        var file = FullPathFile(path, folders);
        return System.IO.File.ReadAllText(file);
    }
    [HttpGet]
    public async Task<FolderToRead[]> GetRootFolders([FromServices] FolderToRead[] folders)
    {
        await Task.Delay(5000);
        return folders;
    }

    static string FullPathFile(string path, [FromServices] FolderToRead[] folders)
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
    [HttpGet("{*path}")]
    public async Task<FileResult> GetFileContent(string path, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(5000);
        var file = FullPathFile(path, folders);
        return PhysicalFile(file, "application/octet-stream");
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
    [HttpGet]
    public async Task<bool> IsFolder(string path, FolderToRead[] folders)
    {
        await Task.Delay(5000);
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
    static DirectoryInfo FolderFromContent(string path,  FolderToRead[] folders)
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
    [HttpGet("{*path}")]
    public async Task<FolderToRead[]> GetFolderContent(string path, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(5000);
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
