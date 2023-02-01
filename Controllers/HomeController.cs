using JobSample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace JobSample.Controllers
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


        [HttpGet]
        public async Task<JsonResult?> Login(LoginModel loginModel)
        {
            ServiceReference.ICUTechClient iCUTechClient = new ServiceReference.ICUTechClient();
            //string Ip = "12345"; // random ip for simplicity
            var result = await iCUTechClient.LoginAsync(loginModel.UserName, loginModel.Password, "12345");

            if (result.@return.ToString().Contains("\"ResultCode\":-1"))
            {
                Response.StatusCode = 400;
                return Json(null);
            }
            else
            {
                return Json( result.@return.ToString() );
            }
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