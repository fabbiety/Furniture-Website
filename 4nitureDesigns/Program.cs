
using Microsoft.EntityFrameworkCore;
using Team.DataBase.Data;
using Team.DataBase.Repository;
using Team.DataBase.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Team.Utilities;
using Stripe;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<TeamBaseContext>(options=>options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefConnection")
    ));
builder.Services.Configure<StripeConfig>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<TeamBaseContext>().AddDefaultTokenProviders();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

//Add Service for registering the repositories unto the indepency injection by using AddScope 
builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();

//Adding Default Cookie path for Authorization purposes
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    
});

//Adding Session to the application to capture activity of the users realtime
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

string key = builder.Configuration.GetSection("Stripe:Secret_Key").Get<string>();
StripeConfiguration.ApiKey = key;

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();         /*To enable the application to use the default session in .Net core*/

app.MapRazorPages();

app.MapControllers();

app.Run();
