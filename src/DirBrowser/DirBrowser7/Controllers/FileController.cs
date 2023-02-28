using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirBrowser7.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class FileController : ControllerBase
{
    [HttpGet]
    public FolderToRead[] GetRootFolders([FromServices] FolderToRead[] folders)
    {
        return folders;
    }
    [HttpGet("{*path}")]
    public FolderToRead[] GetFolderContent(string path,[FromServices] FolderToRead[] folders)
    {
        //todo: html decode
        if (path.Contains("%2f"))
            path = path.Replace("%2f", "/");
        if (path.Contains("%2F"))
            path = path.Replace("%2F", "/");

        var str = path.Split("/");
        if (str.Length == 0)
            throw new ArgumentException($"{nameof(path)} must have data");
        
        var f = folders.FirstOrDefault(it =>

        string.Equals(it.Id, str[0], StringComparison.InvariantCultureIgnoreCase));

        if (f == null)
        {
            var startF = string.Join("|",folders.Select(it => it.Id).ToArray());
            throw new ArgumentException($"{nameof(path)} = {path} must start with folders {startF}");
        }
            var pathFull = f.TransformFullPath;
        for (int i = 1; i < str.Length; i++)
        {
            var path1= str[i];
            pathFull = Path.Combine(pathFull, path1);
            if(!Directory.Exists(pathFull) )
                throw new DirectoryNotFoundException($"{pathFull} must exists");

        }
        var di = new DirectoryInfo(pathFull);
        var files=di.EnumerateFiles().ToArray().Select(it=>new FolderToRead(it)).ToArray();
        var dirs= di.EnumerateDirectories().ToArray().Select(it => new FolderToRead(it)).ToArray();

        List<FolderToRead> ret = new();
        if (dirs.Length > 0)
            ret.AddRange(dirs);
        if (files.Length > 0)
            ret.AddRange(files);
        return ret.ToArray();

    }
}
