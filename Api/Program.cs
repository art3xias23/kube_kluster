using Api;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<Settings>(
    builder.Configuration.GetSection("Settings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
