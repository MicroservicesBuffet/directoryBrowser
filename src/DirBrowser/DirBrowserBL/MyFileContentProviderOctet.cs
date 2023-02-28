namespace DirBrowserBL;
public class MyFileContentProviderOctet : IContentTypeProvider
{

    public bool TryGetContentType(string subpath, out string contentType)
    {
        contentType = "application/octet-stream";
        return true;
    }
}