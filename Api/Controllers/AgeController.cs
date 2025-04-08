using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AgeController : ControllerBase
{
    [HttpGet]
    public int Get([FromQuery] DateTime dob)
    {
        var age = DateTime.Today.Year - dob.Year;
        if (dob.Date > DateTime.Today.AddYears(-age)) age--;
        return age;
    }
}