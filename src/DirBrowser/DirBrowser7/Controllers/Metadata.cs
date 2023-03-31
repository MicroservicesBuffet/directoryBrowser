using Asp.Versioning;
using Generated;
using Microsoft.AspNetCore.Mvc;

namespace DirBrowser7.Controllers;


[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class Metadata : ControllerBase
{

    [HttpPost]
    public async Task<bool> CreateDb([FromServices] ApplicationDBContext context)
    {
        return await context.Database.EnsureCreatedAsync();
    }
}
