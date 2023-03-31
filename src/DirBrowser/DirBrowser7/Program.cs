
using Generated;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//   .AddNegotiate();

//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy.
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



builder.Host.UseNLog();
builder.Services.AddLogging(it =>
{
    it.ClearProviders();
    it.AddNLog();
});
var app = builder.Build();

app.UseCors(it => it
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod()
        .SetIsOriginAllowed(it => true)
        );
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseDirs(app.Configuration);
app.MapControllers();
app.UseBlocklyUI(app.Environment);
app.UseBlocklyAutomation();
app.MapFallbackToFile("/show/{**slug:nonfile}", "index.html");
app.Run();
