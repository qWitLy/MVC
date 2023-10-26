using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication10.Data;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext ac = new(new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MVCDB;Trusted_Connection=True;").Options);
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult GetUsers()
        {
            return View(ac.Users);
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Change(int id)
        {
            User? user = ac.Users.FirstOrDefault(data => data.Id == id);
            if (user == null) return View("GetUsers", ac.Users);
            return View(user);
        }
        [HttpPost]
        public IActionResult Change(User user)
        {
            ac.Users.Update(user);
            ac.SaveChanges();
            return View("GetUsers",ac.Users);

        }
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
        [HttpPost]
        public IActionResult Check(User user)
        {
            Console.Write(user.Name + user.Age);
            ac.Add(user);
            ac.SaveChanges();
            return View("GetUsers",ac.Users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}