using System;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using OP.RemoteAdvisor.Hubs;
using StackExchange.Redis;

namespace OP.RemoteAdvisor
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) => Configuration = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().AddRazorPagesOptions(options =>
      {
        options.RootDirectory = "/src/server/Pages";
      });
      services.AddRazorPages().AddRazorPagesOptions(options =>
      {
        options.Conventions.AddPageRoute("/KioskDesktop", "/kiosk-desktop");
        options.Conventions.AddPageRoute("/KioskTablet", "/kiosk-tablet");

      });
      services.AddSingleton<IUserIdProvider, UserIdProvider>();
      services.AddSignalR();    

      var redisConnectionString = Configuration.GetConnectionString("RedisConnection");

      services.AddDistributedRedisCache(options =>
      {
        options.Configuration = redisConnectionString;
        options.InstanceName = "op-ra";
      });

      services.AddSingleton<IConnectionMultiplexer>(
          ConnectionMultiplexer.Connect(redisConnectionString));

      var dbConnectionString =
          Configuration.GetConnectionString("MongoDBConnection");
      var client = new MongoClient(dbConnectionString);

      services.AddSingleton(client.GetDatabase("op-ra"));
      services.AddLogging();

      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
      .AddCookie(options =>
      {
        options.LoginPath = "/Login";
      });      
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      var rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "public");
      
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(rootFolder)
      });
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapRazorPages();
        endpoints.MapControllers();

        endpoints.MapHub<CallQueueHub>("/callQueue");
        endpoints.MapHub<CallSessionHub>("/callSession");
      });
    }
  }
}
