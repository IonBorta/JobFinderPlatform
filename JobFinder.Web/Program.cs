using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
using JobFinder.Core.Interfaces;
using JobFinder.DAL.Entities;
using JobFinder.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IRepository<Job>, JobRepository>();

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
