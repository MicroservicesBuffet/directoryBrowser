using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirBrowserBL
{
    public class RenderingTemplates
    {
        public string RenderStartFolders(FolderToRead[] flds)
        {
            return FirstRendering.Render(flds);
        }
    }
}
