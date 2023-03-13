using ExamServer.EntityFramework;
using ExamServer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ExamDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ExamDB")));
builder.Services.AddTransient(typeof(ICrudService<>), typeof(CrudService<>));
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
app.UseEndpoints(s=> s.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"));


app.MapRazorPages();

app.Run();
