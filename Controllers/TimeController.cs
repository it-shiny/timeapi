using Microsoft.AspNetCore.Mvc;
using System;

namespace TimeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTime([FromQuery] string timezone = "")
        {
            var currentTime = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(timezone))
            {
                try
                {
                    var adjustedTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentTime, timezone);
                    return Ok(new { currentTime = currentTime.ToString("yyyy-MM-ddTHH:mm:ssZ"), adjustedTime = adjustedTime.ToString("yyyy-MM-ddTHH:mm:ssZ") });
                }
                catch (TimeZoneNotFoundException)
                {
                    return BadRequest("Invalid timezone.");
                }
            }
            else
            {
                return Ok(new { currentTime = currentTime.ToString("yyyy-MM-ddTHH:mm:ssZ") });
            }
        }
    }
}
