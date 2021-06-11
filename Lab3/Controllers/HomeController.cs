using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab3.Models;

namespace Lab3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Razor()
        {
            return View();
        }

        public IActionResult Count()
        {
            ViewBag.theCount = Request.Form["theCount"];
            return View();
        }

        [HttpGet]
        public IActionResult CreatePerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DisplayPerson( Person model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            return Error();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
