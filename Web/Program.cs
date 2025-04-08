using Data;
using Microsoft.EntityFrameworkCore; // Ensure this using directive is present
using Microsoft.Extensions.DependencyInjection; // Add this using directive for service extensions

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/");
});

// Ensure the Microsoft.EntityFrameworkCore.SqlServer package is installed
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddHttpClient("napi", client =>
{
    client.BaseAddress = new Uri("https://localhost:5003/"); //
});
var app = builder.Build();

// Configure the HTTP request pipeline.  
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
  .WithStaticAssets();

app.Run();
