
using DirBrowser7;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var plugins = LoadPlugins().ToArray();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddShortUrl();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterFolders();
builder.Services.AddTransient<IFileOperations,FileOperations>();
builder.Services.AddTransient<IHistoryFileString, HistoryFileString>();
builder.Services.AddTransient<ISearchDataModifiedUserFile, SearchDataModifiedUserFile>();
builder.Services.AddTransient<ISearchDataModifiedFile, SearchDataModifiedFile>();
builder.Services.AddTransient<IFileSearch, FileSearch>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll",
                  builder => builder
                  .SetIsOriginAllowed(it => true)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials());
});

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//   .AddNegotiate();

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = options.DefaultPolicy;
    
//});

var cnString = builder.Configuration.GetConnectionString("ApplicationDBContext");
if (string.IsNullOrWhiteSpace(cnString))
{
    throw new ArgumentException("please add  connection string ApplicationDBContext into appsettings.json ");
}
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    //options.UseSqlServer("Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>;TrustServerCertificate=true;");
    options.UseSqlServer(cnString);
}
);
var folders = builder.Configuration.GetFoldersToRead();
var hc = builder.Services.AddHealthChecks();
hc
    .AddSqlServer(cnString, name: "database SqlServer");

foreach (var item in folders)
{
    hc.AddTypeActivatedCheck<HealthCheckFolder>(item.Id, item.FullPath);
}

    


builder.Services
     .AddHealthChecksUI(setup =>
     {

         var health = "/healthz";
         //if (IsBuildFromCI)
         //{
         //    health = builder.Configuration["MySettings:url"] + health;
         //}
         setup.AddHealthCheckEndpoint("me", health);
         setup.SetEvaluationTimeInSeconds(60);
         //setup.SetHeaderText
         setup.MaximumHistoryEntriesPerEndpoint(10);
     }

    )
     .AddInMemoryStorage()
    ;


builder.Services.AddSingleton<MiddlewareShutdown>();
builder.Host.UseNLog();
builder.Services.AddLogging(it =>
{
    it.ClearProviders();
    it.AddNLog();
});
if (!Environment.UserInteractive)
{
    builder.Services.AddWindowsService(options =>
    {
        options.ServiceName = "DirBrowser";
    });
   

}
builder.Services.AddAuthentication(HttpSysDefaults.AuthenticationScheme);
//var ServicesToRun = new ServiceBase[] { backend };
//ServiceBase.Run(ServicesToRun);
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    builder.WebHost.UseHttpSys(options =>
    {
        options.Authentication.Schemes =
            AuthenticationSchemes.NTLM |
            AuthenticationSchemes.Negotiate;
        options.Authentication.AllowAnonymous = false;
    });
}
builder.Services.AddSingleton(plugins);
var app = builder.Build();

foreach(var item in plugins)
{
    var config = builder.Configuration.GetRequiredSection(item.GetName())!;
    ArgumentNullException.ThrowIfNull(config);
    var dict = config.AsEnumerable().ToDictionary(it => it.Key, it => it.Value);
    ArgumentNullException.ThrowIfNull(dict);
    var data=System.Text.Json.JsonSerializer.Serialize(dict);
    ArgumentNullException.ThrowIfNullOrEmpty(data);
    await item.SetSettings(data);
}
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI(setup =>
{

});

app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseMiddleware<MiddlewareShutdown>();

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");


app.UseAuthorization();
app.UseAuthentication();
app.UseShortUrl();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseDirs(app.Configuration);
app.MapControllers().RequireAuthorization();
app.MapShortUrlEndpoints();
app.UseBlocklyUI(app.Environment);
app.UseBlocklyAutomation();
app.MapUsefullAll();
app.UseAMS();
app.MapGet("/plugins/names", (HttpContext ctx) =>
{
    var names = plugins.Select(it => it.GetName()).ToArray();
    ctx.Response.WriteAsJsonAsync(names);
});
app.MapFallbackToFile("/show/{**slug:nonfile}", "index.html");
await app.RunAsync(UsefullExtensions.UsefullExtensions.cts.Token);

IEnumerable<ISaveFile> LoadPlugins()
{

    var loaders = new List<PluginLoader>();

    // create plugin loaders
    var pluginsDir = Path.Combine(AppContext.BaseDirectory, "plugins");
    if(Directory.Exists(pluginsDir))
    foreach (var dir in Directory.GetDirectories(pluginsDir))
    {
        var dirName = Path.GetFileName(dir);
        var pluginDll = Path.Combine(dir, dirName + ".dll");
        if (File.Exists(pluginDll))
        {
            var loader = PluginLoader.CreateFromAssemblyFile(
                pluginDll,
                sharedTypes: new[] { typeof(ISaveFile), typeof(ILogger) });
            loaders.Add(loader);
        }
    }

    // Create an instance of plugin types
    foreach (var loader in loaders)
    {
        foreach (var pluginType in loader
            .LoadDefaultAssembly()
            .GetTypes()
            .Where(t => typeof(ISaveFile).IsAssignableFrom(t) && !t.IsAbstract))
        {
            // This assumes the implementation of IPlugin has a parameterless constructor
            ISaveFile? plugin = Activator.CreateInstance(pluginType) as ISaveFile;
            //Console.WriteLine($"Created plugin instance '{plugin.GetName()}'.");
            if (plugin != null)
                yield return plugin;
        }
    }
}
