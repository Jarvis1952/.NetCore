using Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<WeatherAPIOptions>(builder.Configuration.GetSection("WeatherAPI"));
var app = builder.Build();
app.UseStaticFiles();
app.MapControllers();
app.UseRouting();
app.Run();
