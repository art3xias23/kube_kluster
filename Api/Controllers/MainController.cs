using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
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
            var psi = new ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = $" Console.dll {param}",
                WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "bin", "Debug", "net9.0"),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = psi };
            process.Start();

            // Read output asynchronously
            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            process.WaitForExit();

            // Return both output and error for debugging
            return Ok(new { output, error });
        }

        [HttpGet("Second")]
        public async Task<IActionResult> Get()
        {
            using var client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5179/Second/Get");

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            return Ok(responseBody);

        }

        [HttpGet("SecondParam")]
        public async Task<IActionResult> Get(string param)
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"http://localhost:5179/Second/GetParam?param={param}");

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            return Ok(responseBody);

        }
    }
}

