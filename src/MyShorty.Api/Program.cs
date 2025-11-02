using Microsoft.EntityFrameworkCore;

using MyShorty.Api.Configuration;
using MyShorty.Api.Models;
using MyShorty.Api.Repositories;
using MyShorty.Api.Repositories.Contexts;
using MyShorty.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddOptions<EncodingOptions>()
        .BindConfiguration(EncodingOptions.SectionKey)
        .ValidateDataAnnotations()
        .ValidateOnStart();

services.AddOptions<ApplicationOptions>()
        .BindConfiguration(ApplicationOptions.SectionKey)
        .ValidateDataAnnotations()
        .ValidateOnStart();

if (builder.Environment.IsDevelopment())
{
  services.AddOpenApi();
}

services.AddDbContextPool<MyShortyContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("MyShortyContext"));
});

services.AddScoped<IMyShortyInterface, MyShortyRepository>();

services.AddScoped<UrlShorteningService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();

app.MapPost("/generateShortUrl", (
  ShortUrlRequest request,
  UrlShorteningService shorteningService) =>
{
  return shorteningService.ShortenUrl(request.Url);
})
.WithName("GenerateShortUrl");

app.MapGet("/{*ShortUrl}", async (
    string ShortUrl,
    UrlShorteningService shorteningService,
    HttpResponse response) =>
{
  var origionalUrl = await shorteningService.GetOriginalUrl(ShortUrl);

  response.Redirect(origionalUrl);
})
.WithName("Redirect");

app.Run();
