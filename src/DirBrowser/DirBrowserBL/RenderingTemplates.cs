namespace DirBrowserBL;

public class RenderingTemplates
{
    public string RenderStartFolders(FolderToRead[]? flds)
    {
        if (flds == null)
            return "no folders";
        return FirstRendering.Render(flds); 
    }
} 
