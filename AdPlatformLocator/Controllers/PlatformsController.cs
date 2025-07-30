using AdPlatformLocator.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdPlatformLocator.Controllers;

[ApiController]
[Route("[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IAdPlatformService _service;

    public PlatformsController(IAdPlatformService service)
    {
        _service = service;
    }
    
    [HttpPost("upload")]
    public IActionResult Upload(IFormFile file)
    {
        if(file.Length == 0)
            return BadRequest("Файл пустой или отсутствует.");
        
        _service.LoadFromFile(file);
        return Ok("Файл успешно загружен.");
    }
    
    [HttpGet("search")]
    public IActionResult Search([FromQuery] string location)
    {
        if (string.IsNullOrWhiteSpace(location))
            return BadRequest("Локация не указана.");
        
        var result = _service.FindPlatformsForLocation(location);
        return Ok(result);
    }
}
