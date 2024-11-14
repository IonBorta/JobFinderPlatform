using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
using JobFinder.Core.DTOs;
using JobFinder.Core.Interfaces;
using JobFinder.DAL.Context;
using JobFinder.DAL.Entities;
using JobFinder.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure ApplicationDbContext with connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly("JobFinder.DAL")
    ));
/*services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("JobFinder.DAL"))); // Specify the migrations assembly*/

builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IRepository<Job>, JobRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILogService<UserDTO>, AccountService>();
builder.Services.AddScoped<IRepository<User>, AccountRepository>();
builder.Services.AddScoped<ILogRepository<User>, AccountRepository>();

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ILogService<CompanyDTO>, CompanyService>();
builder.Services.AddScoped<IRepository<Company>, CompanyRepository>();
builder.Services.AddScoped<ILogRepository<Company>, CompanyRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
