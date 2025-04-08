using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NameController : ControllerBase
{
    [HttpGet]
    public int Get([FromQuery] string name)
    {
        return name?.Length ?? 0;
    }
}