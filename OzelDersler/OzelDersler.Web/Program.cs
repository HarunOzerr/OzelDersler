using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OzelDersler.Business.Abstract;
using OzelDersler.Business.Concrete;
using OzelDersler.Data.Abstract;
using OzelDersler.Data.Concrete;
using OzelDersler.Data.Concrete.EfCore.Contexts;
using OzelDersler.Entity.Concrete.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<OzelDerslerContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<OzelDerslerContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(50);
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = "OzelDersler.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStudentService, StudentManager>();
builder.Services.AddScoped<ITeacherService, TeacherManager>();
builder.Services.AddScoped<IBranchService, BranchManager>();
builder.Services.AddScoped<ICourseService, CourseManager>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "teachers",
    pattern: "branch/{branchurl?}",
    defaults: new {controller = "TeacherList", action = "TeacherList" }
    );

app.MapControllerRoute(
    name: "branches",
    pattern: "branches/{branchurl?}",
    defaults: new { area = "Teachers", controller = "Course", action = "CourseList" }
    );

app.MapControllerRoute(
    name: "branchs",
    pattern: "branslar/{branchurl?}",
    defaults: new { area = "Students", controller = "Course", action = "CourseList" }
    );

app.MapControllerRoute(
    name: "coursedetails",
    pattern: "kurslar/{courseurl}",
    defaults: new { area = "Teachers" , controller = "Course", action = "CourseDetails" }
    );

app.MapControllerRoute(
    name: "coursedetail",
    pattern: "kurs/{courseurl}",
    defaults: new { area = "Students", controller = "Course", action = "CourseDetails" }
    );

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Teachers",
    areaName: "Teachers",
    pattern: "Teachers/{controller=home}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    name: "Students",
    areaName: "Students",
    pattern: "Students/{controller=home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




app.Run();
