using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RevitFamilyBox.FamilyManagementService.Server.Config;
using RevitFamilyBox.FamilyManagementService.Server.Services;
using RevitFamilyBox.FamilyManagementService.Server.Services.Imp;

namespace RevitFamilyBox.FamilyManagementService.Server
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "RevitFamilyBox WebAPI",
          Description = "RevitFamilyBox WebAPI", 
          Version = "v1"
        });
      });

      services.AddControllers();


      services.AddSingleton(sp => {

        var familyMgrCfg = Configuration.GetSection(@"MongoDB:FamilyManager").Get<FamilyManagerConfig>();

        return familyMgrCfg;
      });

      services.AddTransient<IFamilyManagerService, FamilyManagerMongoDBService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RevitFamilyBox WebAPI");
      });

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
