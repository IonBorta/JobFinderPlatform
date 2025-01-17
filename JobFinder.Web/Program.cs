using IronPdf.Extensions.Mvc.Core;
using JobFinder.BLL.Decorator;
using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Proxy;
using JobFinder.BLL.Services;
using JobFinder.Core.DTOs;
using JobFinder.DAL.AbstractFactory.Abstract.Factory;
using JobFinder.DAL.AbstractFactory.Concrete.Factory;
using JobFinder.DAL.Context;
using JobFinder.DAL.Entities;
using JobFinder.Web.Models.Job;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Rotativa.AspNetCore;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// JWT Authentication
/*builder.Services.AddAuthentication( options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    )
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });*/
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();  // Required for session state
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set session timeout duration
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;  // Make sure session is always available
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// Configure ApplicationDbContext with connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly("JobFinder.DAL")
    ));

// Register Concrete Factory
builder.Services.AddScoped<IRepositoryFactory, SqlRepositoryFactory>();

// Registering Serivices
builder.Services.AddScoped<JobService>();
builder.Services.AddScoped<IJobService>(sp =>
{
    var jobService = sp.GetRequiredService<JobService>();
    var memoryCache = sp.GetRequiredService<IMemoryCache>();
    return new CachedJobService(jobService, memoryCache);
});
//builder.Services.AddScoped<IJobService, JobService>();
//builder.Services.AddScoped<IRepository<Job>, JobRepository>();
//builder.Services.AddScoped<IJobRepository<Job>, JobRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
//builder.Services.AddScoped<ILogService<UserDTO>, AccountService>();
//builder.Services.AddScoped<IRepository<User>, AccountRepository>();
//builder.Services.AddScoped<ILogRepository<User>, AccountRepository>();

builder.Services.AddScoped<ApplicationService>();
// Register the proxy service
builder.Services.AddScoped<IApplicationService>(provider => 
{ 
    var realService = provider.GetRequiredService<ApplicationService>();
    var repositoryFactory = provider.GetRequiredService<IRepositoryFactory>();
    var companyService = provider.GetRequiredService<ICompanyService>();
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();  
    return new ApplicationProxy(realService, repositoryFactory,companyService, httpContextAccessor); 
});
//builder.Services.AddScoped<IRepository<ApplicationEntity>, ApplicationRepository>();

builder.Services.AddScoped<ICompanyService, CompanyService>();
//builder.Services.AddScoped<IRepository<Company>, CompanyRepository>();
//builder.Services.AddScoped<IGetByUserRepository<Company>, CompanyRepository>();

builder.Services.AddScoped<JobPageViewModel>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Register IRazorViewRenderer here
builder.Services.AddSingleton<IRazorViewRenderer, RazorViewRenderer>();

var app = builder.Build();

// Configure Rotativa
Rotativa.AspNetCore.RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
