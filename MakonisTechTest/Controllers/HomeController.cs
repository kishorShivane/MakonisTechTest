
using MakonisTechTest.Core;
using MakonisTechTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonService _personService;

        public HomeController(ILogger<HomeController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Index method called");
            return View(await _personService.GetPersonList());
        }


        [HttpGet]
        public IActionResult AddPerson()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(PersonViewModel person)
        {
            if (person is null)
            {
                throw new System.ArgumentNullException(nameof(person));
            }

            if (!ModelState.IsValid)
            {
                return View(person);
            }

            _logger.LogInformation($"AddPerson method :{JsonConvert.SerializeObject(person)}");

            await _personService.AddPerson(person);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new System.ArgumentNullException(nameof(id));
            }

            _logger.LogInformation($"DeletePerson method with ID:{id.ToString()}");

            await _personService.DeletePerson(id);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
