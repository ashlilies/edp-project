using System.Configuration;
using System.Reflection;
using GrowGreenWeb;
using GrowGreenWeb.Data;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<GrowGreenContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:GrowGreenDB"]);
});

builder.Services.AddSession();

// redirect to 403 on Forbid()
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.Events.OnRedirectToAccessDenied = context => {
            context.Response.StatusCode = 403;
            return Task.CompletedTask;
        };
    });

// configure Stripe - for rh
StripeConfiguration.SetApiKey(builder.Configuration.GetSection("Stripe")["SecretKey"]);
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapHub<GrowGreenWeb.Pages.Giving.Chat.SignalRServer>("/chathub");

app.UseRouting();
app.UseSession(); // default timeout: 20 mins
//app.UseStatusCodePagesWithRedirects("/Error?id={0}");
app.UseAuthorization();

app.MapRazorPages();

app.Run();