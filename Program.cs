using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using MvcDemo.Entities;
using MvcDemo.Interfaces;
using MvcDemo.Models;
using MvcDemo.Profiles;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);
//注入数据库框架
builder.Services.AddDbContext<DemoDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

//增加一个默认的HttpContextAccessor
builder.Services.AddHttpContextAccessor();
//增加服务
builder.Services.AddScoped<IDemoService, DemoService>();

//1. 往容器中添加Session服务，启用Session服务
builder.Services.AddSession();

// builder.Services.AddAutoMapper(typeof(AutomapProfile));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutomapProfile>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

//2.使用Session中间件，主要用于拦截Http请求
app.UseSession();
//app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
