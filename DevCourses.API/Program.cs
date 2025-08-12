using DevCourses.API.Data;
using DevCourses.API.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);
//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .Enrich.FromLogContext()
//    .WriteTo.Console(
//        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
//    .CreateLogger();

//builder.Host.UseSerilog();


// Bu örnekte, herhangi bir kaynaktan gelen isteklere izin veriyoruz.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Sağlık kontrolü servislerini ekliyoruz.
// Uygulamanın temel olarak çalışıp çalışmadığını kontrol eder.
builder.Services.AddHealthChecks();
// Custom bir sağlık kontrolü de ekleyebilirsiniz.
builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>("custom_check");


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Weather Forecast API V1",
        Version = "v1",
        Description = "Bu, hava durumu tahmini verilerini sağlayan örnek bir Web API'sidir."
    });

    // API versiyonlamasını desteklemek için v2 belgesi eklenir.
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "Weather Forecast API V2",
        Version = "v2",
        Description = "Bu, API'nin versiyonlanmış ikinci sürümüdür."
    });

    // Kod açıklamalarınızın Swagger'a dahil edilmesi için XML yorumlarını etkinleştirir.
    // Bunun için .csproj dosyasına <GenerateDocumentationFile>true</GenerateDocumentationFile> eklemeniz gerekir.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});


// Swagger/OpenAPI belgeleri için gerekli servisler.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
});


var app = builder.Build();

// HTTP istek işlem hattını yapılandırma.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapHealthChecks("/health");

app.UseHttpsRedirection();

// Tanımladığımız CORS politikasını etkinleştiriyoruz.
app.UseCors("AllowAll");

app.Run();


