using Microsoft.AspNetCore.Mvc;

namespace Api2.Controllers;

[ApiController]
[Route("[controller]")]
public class SecondController : ControllerBase
{
    [HttpGet("Get")]
    public IActionResult Get()
    {
        return Ok("This went well.");
    }

    [HttpGet("GetParam")]
    public IActionResult Get(string param)
    {
        return Ok($"{param}");
    }
}
