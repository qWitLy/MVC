using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication10.Data;
using Microsoft.AspNetCore.Authorization;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext ac = new(new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MVCDB;Trusted_Connection=True;").Options);

		[Authorize]
		public IActionResult GetUsers()
        {
            return View(ac.Users);
        }
        [Authorize]
        public IActionResult GetUsersWithSort(string? sort,string? vector)
        {
            if(sort=="По алфавиту" && vector=="По возрастанию")
            return View("GetUsers", ac.Users.OrderByDescending(option=>option.Name));
            if(sort=="По возрасту" && vector=="По возрастанию")
            return View("GetUsers",ac.Users.OrderBy(option=>option.Age));
            if(sort=="По возрасту" && vector=="По убыванию")
            return View("GetUsers",ac.Users.OrderByDescending(option=>option.Age));
            if(sort=="По алфавиту" && vector=="По убыванию")
            return View("GetUsers", ac.Users.OrderBy(option=>option.Name));




            return View("GetUsers",ac.Users);
        }

		[Authorize]
		public IActionResult CreateUser()
        {
            return View();
        }
		[Authorize]
		[HttpGet]
        public IActionResult Change(int id)
        {
            User? user = ac.Users.FirstOrDefault(data => data.Id == id);
            if (user == null) return View("GetUsers", ac.Users);
            return View(user);
        }
		[Authorize]
		[HttpPost]
        public IActionResult Change(User user)
        {
            ac.Users.Update(user);
            ac.SaveChanges();
            return View("GetUsers",ac.Users);

        }
		[Authorize]
		[HttpPost]
        public IActionResult Delete(int id)
        {
            User? user = ac.Users.Find(id);
            if(user==null)
            return View("GetUsers",ac.Users);
            ac.Users.Remove(user);
            ac.SaveChanges();
            return View("GetUsers",ac.Users);
        }
		[Authorize]
		[HttpPost]
        public IActionResult Check(User user)
        {
            if (ModelState.IsValid)
            {


                ac.Add(user);
                ac.SaveChanges();
                return View("GetUsers", ac.Users);
            }
            return View("CreateUser");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}