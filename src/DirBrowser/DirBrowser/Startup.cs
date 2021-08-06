using DirBrowserBL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirBrowser
{
    public class Startup
    {
        static FolderToRead[] data;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DirBrowser", Version = "v1" });
            });
            services.AddSingleton<FolderToRead[]>(s => data);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DirBrowser v1"));
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles(); 
            var provider = new MyFileContentProviderOctet();
            data = Configuration.GetSection("FoldersToRead").Get<FolderToRead[]>();
            foreach (var item in data)
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(item.FullPath),
                    RequestPath = "/" + item.Id,
                    ContentTypeProvider = provider
                });

                app.UseDirectoryBrowser(new DirectoryBrowserOptions()
                {
                    FileProvider = new PhysicalFileProvider(item.FullPath),
                    RequestPath = new PathString("/" + item.Id)
                });
            }
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
