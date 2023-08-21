using Microsoft.EntityFrameworkCore;
using MvcDemo.Entities;
using MvcDemo.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);
//ע�����ݿ���
builder.Services.AddDbContext<DemoDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});


//����һ��Ĭ�ϵ�HttpContextAccessor
builder.Services.AddHttpContextAccessor();
//���ӷ���
builder.Services.AddScoped<IDemoService, DemoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
