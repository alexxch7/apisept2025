using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using Employees.Frontend.Services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

var backendBaseUrl = builder.Configuration["BackendBaseUrl"] ?? "https://localhost:7037/";

builder.Services.AddHttpClient("api", c =>
{
    c.BaseAddress = new Uri(backendBaseUrl);
    c.DefaultRequestHeaders.Accept.Clear();
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddHttpClient("api-auth", c =>
{
    c.BaseAddress = new Uri(backendBaseUrl);
    c.DefaultRequestHeaders.Accept.Clear();
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddSingleton<TokenService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
