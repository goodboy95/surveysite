using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dao;
using Utils;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace simpleproj
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
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "quizsite API", Version = "0.3" });
                c.IncludeXmlComments(AppContext.BaseDirectory + "/apis.xml");
            });

            services.AddDbContext<DwDbContext>(options => options.UseMySql(Configuration.GetConnectionString("mysql")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DwDbContext dbc)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "quizsite API");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //如/file对应的文件夹不存在，自动创建文件夹
            if (!Directory.Exists(Configuration.GetSection("VirtualPath").GetValue<string>("File")))
            {
                Directory.CreateDirectory(Configuration.GetSection("VirtualPath").GetValue<string>("File"));
            }
            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(Configuration.GetSection("VirtualPath").GetValue<string>("File")),
                RequestPath = "/file",
                EnableDirectoryBrowsing = false
            });
            Initialize.DbInit(dbc);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
