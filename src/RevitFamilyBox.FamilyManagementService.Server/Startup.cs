using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RevitFamilyBox.FamilyManagementService.Server.Config;
using RevitFamilyBox.FamilyManagementService.Server.Domain;
using RevitFamilyBox.FamilyManagementService.Server.Repository;

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

      services.AddTransient<IFamilyInfoRepository, MongoFamilyInfoRepository>();
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
