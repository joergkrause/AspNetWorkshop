using TheDashboard.DatabaseLayer.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Quartz;
using Quartz.AspNetCore;
using TheDashboard.BuildingBlocks.Extensions;
using TheDashboard.DataConsumerService.BusinessLogic;
using TheDashboard.DataConsumerService.Domain;
using TheDashboard.DataConsumerService.Infrastructure;
using TheDashboard.DataConsumerService.Jobs;
using TheDatabase.DataConsumerService.BusinessLogic.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultServices();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataConsumerDbContext>(opt =>
{
  opt.UseSqlServer(cs);
});

builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IDataConsumerService, DataConsumerService>();

builder.Services.AddEventbus<DataConsumerDbContext>(builder.Configuration, nameof(DashboardService));
// add eventbus channel for direct publish

builder.Services.AddQuartz(config =>
{
  config.UseMicrosoftDependencyInjectionJobFactory();

  // configure persistence with EF Core
  config.UsePersistentStore(opt =>
  {    
    opt.UseProperties = true;
    opt.RetryInterval = TimeSpan.FromSeconds(15);
    opt.UseClustering();
    opt.UseSqlServer(ado => {
      ado.TablePrefix = "QRTZ_";
      ado.ConnectionString = cs;      
    });
    opt.UseJsonSerializer();
  });

  var consumerKey = new JobKey("ConsumerJob");
  config.AddJob<ConsumerJob>(opt => opt.WithIdentity(consumerKey));

  config.AddTrigger(opt => opt
     .ForJob(consumerKey)
        .WithIdentity("ConsumerJobTrigger")
           .WithCronSchedule("0/5 * * * * ?"));
});

builder.Services.AddTransient<ConsumerJob>();

builder.Services.AddQuartzHostedService(config =>
{
  config.WaitForJobsToComplete = true;
});

builder.Services.AddSwaggerGen(config =>
{
  config.SwaggerDoc("v1", new() { Title = "DataConsumer API", Version = "v1" });
});
builder.Services.AddHealthChecks();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(config =>
{
  config.SwaggerEndpoint("/swagger/v1/swagger.json", "DataConsumer API v1");  
});

app.UseHttpsRedirection();

await app.ExecuteMigration<DataConsumerDbContext, Dashboard, Guid>(async (ctx, _) => await SeedDatabase.Seed(ctx));

app.Run();

