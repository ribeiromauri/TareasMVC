using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using TareasMVC;

var builder = WebApplication.CreateBuilder(args);

var politicaUsuarios = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

// Add services to the container.
builder.Services.AddControllersWithViews(opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter(politicaUsuarios));
});

//configurar db context como un servicio y pasarle el connectionString 
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

//Configuracion despues de instalar Identity 
//AddIdentity usa la clase por defecto de IdentityUser
builder.Services.AddAuthentication();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;
}
).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//Configuracion para cambiar path por defecto
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, opciones =>
{
    opciones.LoginPath = "/usuarios/login";
    opciones.AccessDeniedPath = "/usuarios/login";
});

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
