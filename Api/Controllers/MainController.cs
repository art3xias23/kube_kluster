using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Runtime;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private readonly Settings _settings;
        public MainController(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        [HttpGet("Crash")]
        public IActionResult Crash()
        {
            // Kill the pod after 2 second delay
            _ = Task.Run(async () =>
            {
                await Task.Delay(2000);
                Environment.Exit(1);
            });

            return Ok(new { message = "Pod will crash in 2 seconds" });
        }

        [HttpGet("Call")]
        public IActionResult Simple()
        {
            return Ok(new { message = "This went well." });
        }

        [HttpGet("Console")]
        public async Task<IActionResult> Console(string param)
        {
            var cw = Directory.GetCurrentDirectory();
            var isContainer = System.IO.File.Exists("/.dockerenv");
            System.Console.WriteLine($"isContainer: {isContainer}");
            var psi = new ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = $"Console/Console.dll {param}",
                WorkingDirectory = "./",
            };

            using var process = new Process { StartInfo = psi };
            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            process.WaitForExit();

            return Ok(new { output, error });
        }

        [HttpGet("Second")]
        public async Task<IActionResult> Get()
        {
            string apiUrl = Environment.GetEnvironmentVariable("SETTINGS__SECOND_API_URL");
            using var client = new HttpClient();

            var response = await client.GetAsync($"{_settings.SECOND_API_URL}Second/Get");

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            return Ok(responseBody);

        }

        [HttpGet("SecondParam")]
        public async Task<IActionResult> Get(string param)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{_settings.SECOND_API_URL}Second/GetParam?param={param}");

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            return Ok(responseBody);

        }
    }
    }

