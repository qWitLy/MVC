using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication10.Data;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
	public class AthorizationController : Controller
	{
		//DataBase
	private readonly ApplicationContext db = new(new DbContextOptionsBuilder<ApplicationContext>()
			.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MVCDB;Trusted_Connection=True;").Options);
		//DataBase
		public IActionResult Registration()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Check(AuthorizeUser user)
		{
			if (ModelState.IsValid)
			{
				db.Add(user);
				db.SaveChanges();
				return View("Login");
			}
			return View("Registration");
		}
		public IActionResult Login()
		{
			return View();
		}
	}
}
