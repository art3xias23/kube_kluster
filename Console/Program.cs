using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Data;

var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseSqlServer("your-connection-string") // replace with your real connection string
    .Options;

using var context = new ApplicationDbContext(options);

// Ensure the folder exists
var saveDir = Path.Combine(Directory.GetCurrentDirectory(), "UserSaves");
Directory.CreateDirectory(saveDir);

// Save each user to a separate file
foreach (var user in context.Users)
{
    var safeName = user.Name.Replace(" ", "_"); // optional: sanitize filename
    var dobFormatted = user.Dob.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
    var fileName = $"user_{dobFormatted}_{safeName}.txt";
    var fullPath = Path.Combine(saveDir, fileName);

    var content = $"Name: {user.Name}\nDOB: {user.Dob:yyyy-MM-dd}";
    File.WriteAllText(fullPath, content);

    Console.WriteLine($"Saved: {fileName}");
}

