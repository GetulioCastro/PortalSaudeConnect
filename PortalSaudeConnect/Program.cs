using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PortalSaudeConnect.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SomenteClinicaOrigem", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("TipoAcessoPortal", "True");
    });

    options.AddPolicy("SomenteClinicaDestino", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("TipoAcessoPortal", "False");
    });

    options.AddPolicy("QualquerClinicaAutenticada", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("TipoAcessoPortal");
    });
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<PortalContext>((optionsAction) =>
{
    var connString = builder.Configuration.GetConnectionString("PortalConnection");
    optionsAction.UseSqlServer(connString);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
