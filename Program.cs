using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using WebApplication10.Controllers;
using WebApplication10.Data;
using WebApplication10.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication("Cookies").AddCookie(option => option.LoginPath = "/Athorization/Login");
builder.Services.AddAuthorization();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=GetUsers}/{id?}");


//сама авторизаци€
app.MapPost("/Athorization/Login", async (string? returnURl, HttpContext context,ApplicationContext db) =>
{
	var form = context.Request.Form;
	if (!form.ContainsKey("Login") || !form.ContainsKey("Password")) return Results.BadRequest("Ћогин или пароль не установлен");

	string? login = form["Login"];
	string? password= form["Password"];
	AuthorizeUser? user = db.AuthorizeUsers.FirstOrDefault(p => p.Login == login && p.Password == password);
	if (user is null) return Results.Unauthorized();

	var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Login) };

	ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
	await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));
	return Results.Redirect("/Home/GetUsers");
});
//Exit
app.MapGet("/Logout", async (HttpContext context) =>
{
	await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
	return Results.Redirect("/Athorization/Login");
});

app.Run();
