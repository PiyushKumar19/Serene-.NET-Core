using Microsoft.EntityFrameworkCore;
using Serene.DataAccess.Data;
using Serene.DataAccess.Repository;
using Serene.DataAccess.Repository.IRepositories;
using Serene.InterfacesAndRepos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultString"));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });
builder.Services.AddTransient<ICategory, CategoryRepo>();
// Generic One
builder.Services.AddScoped<ICategoryRepo, CategoryRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
