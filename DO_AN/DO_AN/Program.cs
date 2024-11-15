using DO_AN.Helpers;
using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ** Configure DbContext **
// Đăng ký DbContext để kết nối với cơ sở dữ liệu, sử dụng chuỗi kết nối trong appsettings.json
builder.Services.AddDbContext<DoAnContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ** Configure Identity **
// Cấu hình ASP.NET Identity để sử dụng User và Role tùy chỉnh của bạn
builder.Services.AddIdentity<User, IdentityRole>()
    .AddDefaultTokenProviders()   // Cung cấp các token mặc định như xác thực
    .AddDefaultUI()               // Cung cấp giao diện mặc định cho các trang đăng nhập, đăng ký
    .AddEntityFrameworkStores<DoAnContext>(); // Kết nối Identity với DbContext

// ** Configure Application Cookie **
// Cấu hình cookie cho các chức năng đăng nhập, đăng xuất và truy cập bị từ chối
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

// ** Add Razor Pages **
// Đăng ký Razor Pages để sử dụng cho các trang giao diện mặc định của Identity
builder.Services.AddRazorPages();

// ** Register Repositories **
// Đăng ký các Repository để phục vụ cho việc truy xuất dữ liệu trong ứng dụng
builder.Services.AddScoped<IDiemkhoihanhRepository, EFDiemkhoihanhRepository>();
builder.Services.AddScoped<IKhachhangRepository, EFKHachhangRepository>();
builder.Services.AddScoped<IPhuongtiendcRepository, EFPhuongtiendcRepository>();
builder.Services.AddScoped<ILoaitourRepository, EFLoaitourRepository>();
builder.Services.AddScoped<IKhuyenmaiRepository, EFKhuyenmaiRepository>();
builder.Services.AddScoped<INhanvienRepository, EFNhanvienRepository>();
builder.Services.AddScoped<IHinhanhRepository, EFHinhanhRepository>();
builder.Services.AddScoped<IDanhlamtcRepository, EFDanhlamtcRepository>();
builder.Services.AddScoped<IDiemdenRepository, EFDiemdenRepository>();
builder.Services.AddScoped<IDiemthamquanRepository, EFDiemthamquanRepository>();
builder.Services.AddScoped<IKhachsanRepository, EFKhachsanRepository>();
builder.Services.AddScoped<ILichtrinhRepository, EFLichtrinhRepository>();
builder.Services.AddScoped<ITourRepository, EFTourRepository>();
builder.Services.AddScoped<IHuongdanRepository, EFHuongdanRepository>();
builder.Services.AddScoped<ICtptRepository, EFCtptRepository>();
builder.Services.AddScoped<ICtddRepository, EFCtddRepository>();
builder.Services.AddScoped<ICtDtqRepository, EFCtDtqRepository>();
builder.Services.AddScoped<IDanhgiaRepository, EFDanhgiaRepository>();
builder.Services.AddScoped<IPhieudattourRepository, EFPhieudattourRepository>();
builder.Services.AddScoped<IVnPayRepository, EFVnPayReponsitory>();

// ** Add MVC Controllers with Views **
// Đăng ký dịch vụ cho các Controller và Views
builder.Services.AddControllersWithViews();

// Thêm SignalR vào dịch vụ
builder.Services.AddSignalR();

var app = builder.Build();

// ** Configure the HTTP request pipeline **
if (!app.Environment.IsDevelopment())
{
    // Trong môi trường không phải phát triển, sử dụng Error Handler và HSTS
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection(); // Chuyển hướng tất cả yêu cầu HTTP sang HTTPS
app.UseStaticFiles(); // Cho phép phục vụ các tệp tĩnh (như hình ảnh, CSS, JavaScript)

app.UseRouting(); // Bắt đầu quá trình định tuyến

// ** Authentication và Authorization **
app.UseAuthentication();  // Xác thực người dùng
app.UseAuthorization();   // Phân quyền truy cập

// Cấu hình SignalR Hub
app.MapHub<MessageHub>("/messageHub");

// ** Configure Endpoints **
// Định nghĩa các đường dẫn của các Controller (để ánh xạ các yêu cầu vào Controller)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Định nghĩa các đường dẫn cho khu vực (Area) nếu có
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Định tuyến cho Razor Pages (bao gồm Identity)

// ** Run Application **
// Chạy ứng dụng
app.Run();
