using Showcase.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDbContext();
builder.AddCors();
builder.AddDocumentation();
builder.AddServices();
builder.AddSecurity();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.OnConfigureDevEnvironment();
app.UseMiddleware();
app.AddSecurity();
app.MapControllers();

app.Run();
