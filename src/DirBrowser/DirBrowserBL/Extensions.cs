using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirBrowserBL;

public static class Extensions
{
    static FolderToRead[]? data;

    public static IServiceCollection RegisterFolders(this IServiceCollection services)
    {
        services.AddDirectoryBrowser();
        services.AddSingleton(s => data);
        return services;
    }
    public static IApplicationBuilder UseDirs(this IApplicationBuilder app, IConfiguration config)
    {
        var provider = new MyFileContentProviderOctet();
        var sect = config.GetSection("FoldersToRead");
        if(sect == null)
            throw new ArgumentException("no section configured");

        data =sect.Get<FolderToRead[]>();
        if((data?.Length??0) == 0)
        {
            throw new ArgumentException("no folders configured");
        }
        
        foreach (var item in data!)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalSearchFileProvider(item.TransformFullPath),
                RequestPath = "/" + item.Id,
                ContentTypeProvider = provider
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalSearchFileProvider(item.TransformFullPath),
                RequestPath = new PathString("/" + item.Id),
                Formatter = new PluginFormatterRoot(item.TransformFullPath, item.Id)
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalSearchFileProvider(item.TransformFullPath),
                RequestPath = new PathString("/json/" + item.Id),
                Formatter = new JsonFormatter(item.TransformFullPath, item.Id)
            });
        }
        app.Map("/dirs", appB =>
        {
            var flds = appB.ApplicationServices.GetRequiredService<FolderToRead[]>();
            appB.Run(async cnt =>
            {
                cnt.Response.ContentType = "text/html";
                await cnt.Response.WriteAsync(new RenderingTemplates().RenderStartFolders(flds));
            });
        });

        app.Map("/json/dirs", appB =>
        {
            var flds = appB.ApplicationServices.GetRequiredService<FolderToRead[]>();
            appB.Run(async cnt =>
            {
                var jsonString = System.Text.Json.JsonSerializer.Serialize(flds);
                cnt.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                await cnt.Response.WriteAsync(jsonString, Encoding.UTF8);
            });
        });
        return app;
    } 
}
