using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PortableDotNetCore.Models;

using DotNetStandardLib;

namespace PortableDotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomePageHeaderService _homePageHeaderService;

        public HomeController(IHomePageHeaderService homePageHeaderService)
        {
            if(homePageHeaderService == null) throw new ArgumentNullException(nameof(homePageHeaderService));
            this._homePageHeaderService = homePageHeaderService;
        }

        public IActionResult Index()
        {
            string homePageHeaderText = this._homePageHeaderService.GetHomePageHeaderText();
            return View(new HomePageModel(homePageHeaderText));
        }

        public class HomePageModel
        {
            public string Header { get; set; }

            public HomePageModel(string header)
            {
                if(String.IsNullOrEmpty(header)) throw new ArgumentNullException(nameof(header));
                this.Header = header;
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
