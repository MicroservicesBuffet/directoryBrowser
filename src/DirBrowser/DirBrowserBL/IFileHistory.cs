﻿namespace DirBrowserBL;

public interface IFileHistory
{
    public string? User { get; set; }
    public byte[]? Content { get; set; }
    public string? FileName { get; set; }
    public string KeyHistory();
    public DateTimeOffset LastModified { get; }

}
