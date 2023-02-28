namespace DirBrowserBL;

public class RenderingTemplates
{
    public string RenderStartFolders(FolderToRead[] flds)
    {
        return FirstRendering.Render(flds);
    }
}
