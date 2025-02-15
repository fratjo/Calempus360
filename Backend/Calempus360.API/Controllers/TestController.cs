using Calempus360.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("error")]
    public IActionResult Get()
    {
        throw new TestException("Test exception");
    }
}