using System.IO;

namespace DirBrowserBL;
public class FolderToRead : IFileInfo, IFileHistory
{
    public FolderToRead()
    {
        Id = "";
        FullPath = "";
    }
    
    public FolderToRead(DirectoryInfo di)
    {
        this.Id= di.Name;
        this.FullPath = di.FullName;
    }
    public FolderToRead(FileInfo fi)
    {
        this.Id = fi.Name;
        this.FullPath = fi.FullName;
    }
    public string Id { get; set; }
    public string FullPath { get; set; }

    public string TransformFullPath
    {
        get
        {
            if (FullPath == "./")
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            }
            return FullPath;
        }
    }


    public bool Exists => true;

    public long Length
    {
        get
        {
            if (IsDirectory)
                return -1;

            return new FileInfo(FullPath).Length;
        }
    }

    public string PhysicalPath => FullPath;

    public string Name => Id;
    private DateTimeOffset? lastModifiedSet = null;
    public DateTimeOffset LastModified 
    {
        get 
        {
            if (lastModifiedSet.HasValue)
                return lastModifiedSet.Value;

            if (IsDirectory)
            {
                return Directory.GetLastWriteTime(FullPath);
            }
            else
            {
                return File.GetLastWriteTime(FullPath);
            }
        }
        set {
            lastModifiedSet = value;

        }
    } 

    public bool IsDirectory => Directory.Exists(FullPath);

    public string? User { get ; set ; }
    public byte[]? Content { get ; set ; }
    public string? FileName { get 
        {
            return TransformFullPath;
        }
        set {
            FullPath = value??"";
        }
    }
    
    public string KeyHistory()
    {
        return this.PhysicalPath + "/" + this.LastModified.ToString("r");
    }
    public Stream CreateReadStream()
    {
        if (IsDirectory)
            throw new ArgumentException("is folder");
        return File.OpenRead(FullPath);
    }

    
}