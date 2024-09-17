using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppointMeWeb.Infrastrucure.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddApplicationService();

var app = builder.Build();

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "logout",
        pattern: "logout",
        defaults: new { controller = "User", action = "Logout" }
    );

    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

await app.RunAsync();
