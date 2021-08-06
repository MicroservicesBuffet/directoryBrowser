using DirBrowserBL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirBrowser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        public IndexController()
        {

        }

        [HttpGet]
        public ContentResult Get([FromServices] FolderToRead[] flds)
        {
            return Content(new RenderingTemplates().RenderStartFolders(flds),"text/html");
        }
    }
}
