using DocStream.API;
using DocStream.Core.Interfaces;
using DocStream.Core.Services;
using DocStream.Data;
using DocStream.Data.Configuration;
using DocStream.Models;
using DocStream.Services.Interfaces;
using DocStream.Services.Services;
using DocStream.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(path: "C:\\DocStream\\logs\\log-.txt",
        outputTemplate:
        "{Timestamp: yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:1j}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information
    ).CreateLogger();
try
{
    Log.Information("Application is starting");
    CreateHostBuilder(args).Build().Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}
static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.Start();
        });



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("name=sqlConnection"));

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("DocStreamApi")
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);


builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

builder.Services.AddAutoMapper(typeof(AutoMapperInitializer));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddTransient<IEmailSender, MailJetEmailSender>();
builder.Services.AddHttpClient<IHttpService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllers().AddNewtonsoftJson(op =>
op.SerializerSettings.ReferenceLoopHandling =
Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "RYTLP-UserManagement", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @" JWT Authorization header using bearer scheme, 
                               enter 'Bearer' follow by Space and enter your token ",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference(){
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "0auth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    });

var app = builder.Build();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
});

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
