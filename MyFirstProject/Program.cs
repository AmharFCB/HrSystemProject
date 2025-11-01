using MyFirstProject.Data;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Repository;
using MyFirstProject.Interfaces;
using MyFirstProject.Interfaces.IServices;
using MyFirstProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<>), typeof(MainRepository<>));
builder.Services.AddScoped(typeof(IRepositoryAttendance), typeof(RepositoryAttendance));
builder.Services.AddScoped(typeof(IRepositoryDepartments), typeof(RepositoryDepartments));
builder.Services.AddScoped(typeof(IRepositoryEmployees), typeof(RepositoryEmployees));
builder.Services.AddScoped(typeof(IRepositoryJobs), typeof(RepositoryJobs));
builder.Services.AddScoped(typeof(IRepositoryLeaveRequests), typeof(RepositoryLeaveRequests));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IAttendanceServices), typeof(AttendanceServices));
builder.Services.AddScoped(typeof(IDepartmentsServices), typeof(DepartmentsServices));
builder.Services.AddScoped(typeof(IEmployeesServices), typeof(EmployeesServices));
builder.Services.AddScoped(typeof(ILeaveRequestsServices), typeof(LeaveRequestsServices));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HrDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=account}/{action=login}/{id?}");

app.Run();
