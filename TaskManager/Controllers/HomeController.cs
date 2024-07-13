using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Models.ViewModels;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AssignTask()
        {
            var model = new AssignTaskViewModel
            {
                //Batches = new List<string> { "Batch1", "Batch2", "Batch3" },
                Students = new List<string> { "Student1", "Student2", "Student3" }
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AssignTask(AssignTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle form submission, save data, etc.
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
