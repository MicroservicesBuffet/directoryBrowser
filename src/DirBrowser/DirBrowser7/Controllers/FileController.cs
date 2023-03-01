using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace DirBrowser7.Controllers;
    
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class FileController : ControllerBase
{
    private readonly FileOperations fo;

    public FileController(FileOperations fo, IHistoryFileString historyFileString)
    {
        this.fo = fo;
    }
    [HttpGet("{*path}")]
    public async Task<IFileHistory[]> GetFileHistory(string path, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(5000);
        return fo.GetFileHistory(path, folders);
    }
    [HttpGet("{*path}")]
    public async Task<string> GetFileText(string path, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(5000);
        return await fo.GetFileText(path,folders);
    }
    [HttpPost]
    public async Task<int> SetFileText([FromBody] SaveTextFile save, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(5000);
        return await fo.SetFileText(save, folders);
    }
    [HttpGet]
    public async Task<FolderToRead[]> GetRootFolders([FromServices] FolderToRead[] folders)
    {
        await Task.Delay(5000);
        return folders;
    }

    [HttpGet("{*path}")]
    public async Task<FileResult> GetFileContent(string path, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(5000);
        var file = fo.FullPathFile(path, folders);
        return PhysicalFile(file, "application/octet-stream");
    }
    
    [HttpGet]
    public async Task<bool> IsFolder(string path, FolderToRead[] folders)
    {
        await Task.Delay(5000);
        return fo.IsFolder(path, folders);
        
    }[HttpGet("{*path}")]
    public async Task<FolderToRead[]> GetFolderContent(string path, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(5000);
        return fo.GetFolderContent(path, folders);

    }
}
