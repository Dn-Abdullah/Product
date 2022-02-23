using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;
using WebApplication11.Repository;
using WebApplication11.ViewModels;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AuthenticationDbContextContextConnection");
builder.Services.AddDbContext<DatabaseContaxt>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 28))));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DatabaseContaxt>();
//builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductUserRepository, ProductUserRepository>();
builder.Services.AddTransient<IProductAdminRepository, ProductAdminRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

builder.Services.AddControllersWithViews();

//builder.Services.AddDefaultIdentity<appiUser>(
//    options => options.SignIn.RequireConfirmedAccount = true
//  )
//.AddEntityFrameworkStores<DatabaseContaxt>();



//builder.Services.AddDbContext<DatabaseContaxt>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContaxt")));
// Add services to the container.


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
