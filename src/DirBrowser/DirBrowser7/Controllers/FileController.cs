﻿using Asp.Versioning;
using Generated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DirBrowser7.Controllers;
[EnableCors("AllowAll")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class FileController : ControllerBase
{
    private readonly IFileOperations fo;

    public FileController(IFileOperations fo)
    {
        this.fo = fo;
    }
    [HttpGet("{*path}")]
    public async Task<IFileHistory[]?> GetFileHistory(string path, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(1000);
        return await fo.GetFileHistory(path, folders);
    }
    [HttpGet("{*path}")]
    public async Task<string> GetFileText(string path, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(1000);
        return await fo.GetFileText(path,folders);
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<int> SetFileText([FromBody] SaveTextFile save, [FromServices] FolderToRead[] folders, [FromServices] ISaveFile[] plugins )
    {
        await Task.Delay(1000);
        
        var user = this.User?.Identity?.Name??"no auth user";
        return await fo.SetFileText(user,save, folders, plugins);
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<FolderToRead[]> GetRootFolders([FromServices] FolderToRead[] folders)
    {
        await Task.Delay(1000);
        return folders;
    }

    [HttpGet("{*path}")]
    [AllowAnonymous]
    public async Task<FileResult> GetFileContent(string path, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(1000);
        var file = fo.FullPathFile(path, folders);
        return PhysicalFile(file, "application/octet-stream");
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<bool> IsFolder(string path, FolderToRead[] folders)
    {
        await Task.Delay(1000);
        return fo.IsFolder(path, folders);
        
    }
    
    [HttpGet("{*path}")]
    [AllowAnonymous]
    public async Task<FolderToRead[]> GetFolderContent(string path, [FromServices] FolderToRead[] folders)
    {
        await Task.Delay(1000);
        return fo.GetFolderContent(path, folders);

    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<FileResult?> GetFileHistory(long id)
    {
        await Task.Delay(1000);
        var data= await fo.GetFileContents(id);
        if (data == null || data.Content == null)
            return null;
        var f = Path.GetFileName(data.FileName);
        var cnt = Encoding.UTF8.GetBytes(data.Content);
        return File(cnt, "application/octet-stream", data.User + "_" + data.LastModified.ToString("yyyyMMddHHmmss") + "_" + f);
    }
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<long> GetFileHistoryCount([FromServices] ISearchDataModifiedUserFile search, long id)
    {

        var s = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Equal, eModifiedUserFileColumns.IDFile, id.ToString());
        return await search.GetAllCount(s);       
    }

    [HttpGet("{line}/{*path}")]
    [AllowAnonymous]
    public async Task<FolderToRead[]> Search([FromServices] IFileSearch search, [FromServices] FolderToRead[] rootFolders, string line, string path)
    {
        try
        {
            var data = await search.SearchFiles(path, line, rootFolders);
            return data;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
