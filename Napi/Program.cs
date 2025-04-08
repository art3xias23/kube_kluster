using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5003);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors();
app.MapControllers();
app.Run();
