using ByteSizeLib;
using EmovisionBlazor.Areas.Identity;
using EmovisionBlazor.Domain;
using EmovisionBlazor.Domain.Models;
using EmovisionBlazor.Hubs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EmovisionDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR(options =>
{
    // Increase the maximum message size to 10 MiB.
    options.MaximumReceiveMessageSize = (long)ByteSize.FromMebiBytes(10).Bytes;
});
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

//builder.Services.AddScoped<IMediaStreamsApi, MediaStreamsApiService>(
//    p => new MediaStreamsApiService(p.GetRequiredService<IJSRuntime>())
//);

builder.Logging.SetMinimumLevel(LogLevel.Trace);

builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<VideoStreamHub>(VideoStreamHub.Uri);

app.Run();
