using B2B_SiteListing.Data.DbContext;
using B2B_SiteListing.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using B2B_SiteListing.Data.Repositories;
using B2B_SiteListing.Data.Repositories.Implementations;
using B2B_SiteListing.Service.Contracts;
using B2B_SiteListing.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

//db context
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("LocalConn")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(opt => {
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = false;
    opt.User.RequireUniqueEmail = true;
    opt.User.AllowedUserNameCharacters = String.Empty;
   // opt.Lockout.MaxFailedAccessAttempts = 3;
    opt.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(10);
});
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Auth/Login";
});

//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddScoped<IRepository<LogInDetails>, Repository<LogInDetails>>();
builder.Services.AddTransient<ILogInDetailsService, LogInDetailsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
