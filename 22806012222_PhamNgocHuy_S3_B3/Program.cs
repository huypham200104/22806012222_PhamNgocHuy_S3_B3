using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using _22806012222_PhamNgocHuy_S3_B3.Data;
using _22806012222_PhamNgocHuy_S3_B3.Repository;
using _22806012222_PhamNgocHuy_S3_B3.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();










// Cấu hình Identity với ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Yêu cầu xác nhận tài khoản nếu cần
})
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Cấu hình DbContext (chỉ cần một lần)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Thêm Razor Pages
builder.Services.AddRazorPages();

// Thêm các dịch vụ Repository
builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();

// Thêm Database Developer Page Exception Filter (dùng trong môi trường Development)
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Cấu hình cookie xác thực
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Sửa LogoutPath thành AccessDeniedPath
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Thay MapStaticAssets() bằng UseStaticFiles() nếu không dùng StaticFiles riêng
app.UseRouting();

app.UseAuthentication(); // Đảm bảo gọi trước UseAuthorization
app.UseAuthorization();

// Định tuyến
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();