using TheDashboard.UiInfoService.Hubs;

namespace TheDashboard.UiInfoService
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      builder.Services.AddSignalR();

      builder.Services.AddAuthorization();


      var app = builder.Build();

      // Configure the HTTP request pipeline.

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapHub<InfoHub>("/Info");

      app.Run();
    }
  }
}