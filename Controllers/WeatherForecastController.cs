using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task GET()
        {
            Response.StatusCode = 200;
            Response.Headers.Append("Cache-Control", "no-cache");
            Response.Headers.Append("Content-Type", "text/event-stream");
            //Response.Headers.Append("Connection", "keep-alive");
           
            var writer = new StreamWriter(Response.Body);
            for (int i = 0; i < 1000; i++)
            {
                await writer.WriteAsync($"data: {i}\n\n");
                await writer.FlushAsync();
                await Task.Delay(1000);
            }

            string doneEvent = "data: [DONE]\n\n";
            await writer.WriteLineAsync(doneEvent);
            await writer.FlushAsync();
        }
    }
}
