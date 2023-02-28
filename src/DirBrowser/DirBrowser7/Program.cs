
using Microsoft.Extensions.Configuration;
using NetCore2BlocklyNew;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterFolders();

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//   .AddNegotiate();

//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy.
//    options.FallbackPolicy = options.DefaultPolicy;
//});

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
app.Run();