using System.ServiceProcess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterFolders();
builder.Services.AddTransient<FileOperations>();
builder.Services.AddTransient<IHistoryFileString, HistoryFileString>();
builder.Services.AddTransient<ISearchDataModifiedUserFile, SearchDataModifiedUserFile>();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
    
});

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
    //var ServicesToRun = new ServiceBase[] { backend };
    //ServiceBase.Run(ServicesToRun);

}

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<MiddlewareShutdown>();

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(it => it
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod()
        .SetIsOriginAllowed(it => true)
        );

app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseDirs(app.Configuration);
app.MapControllers();
app.UseBlocklyUI(app.Environment);
app.UseBlocklyAutomation();
app.MapUsefullAll();
app.UseAMS();

app.MapFallbackToFile("/show/{**slug:nonfile}", "index.html");
await app.RunAsync(UsefullExtensions.UsefullExtensions.cts.Token);
