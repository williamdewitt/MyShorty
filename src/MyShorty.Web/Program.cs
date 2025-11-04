using MyShorty.Web.Clients;
using MyShorty.Web.Components;
using MyShorty.Web.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRefitClient<IMyShortyClient>()
    .ConfigureHttpClient(c=> c.BaseAddress = new Uri("http://localhost:5000"));

builder.Services.AddScoped<MyShortyService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
