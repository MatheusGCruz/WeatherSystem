using WeatherSystem_HangfireWorker;

using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        
        // Configure Entity Framework
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Configure Hangfire
        services.AddHangfire(config => config.UseSqlServerStorage(connectionString));
        services.AddHangfireServer();

        // Register services
        services.AddHttpClient<ApiService>();
        //services.AddSingleton<JobService>();
        services.AddScoped<JobService>();
        services.AddScoped<DatabaseService>();
        services.AddHostedService<Worker>();

        // Register Hangfire Job Manager
        services.AddTransient<IRecurringJobManager, RecurringJobManager>();
    })
    .Build();

using (var scope = builder.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

using (var scope = builder.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    var jobService = scope.ServiceProvider.GetRequiredService<JobService>();
    
    // Schedule the job to run every minute
    RecurringJob.AddOrUpdate("fetch-api-job", () => jobService.FetchAndSaveDataAsync(), Cron.Hourly);
}

await builder.RunAsync();
