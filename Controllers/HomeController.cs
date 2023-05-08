using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;
#pragma warning disable CS8618

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public ViewResult Index()
    {
        return View("Index");
    }

    [HttpPost("process")]
    public IActionResult Process(string Name)
    {
        if (Name == null)
        {
            return View("Index");
        }
        else
        {
            HttpContext.Session.SetString("Name",Name);
            return RedirectToAction("Dashboard");
        }
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        string userInSession = HttpContext.Session.GetString("Name");
        if (userInSession == null){
            return RedirectToAction("Index");
        }
        int? currentNumber = HttpContext.Session.GetInt32("number");
        if (currentNumber == null){
            HttpContext.Session.SetInt32("number",22);
        }
        return View("Dashboard");
    }

    [HttpPost("logout")]
    public RedirectToActionResult Logout ()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [HttpPost("math")]
    public RedirectToActionResult Math(string action)
    {
        int? currentNumber = HttpContext.Session.GetInt32("number");
        if (action == "add1"){
            HttpContext.Session.SetInt32("number",(int)currentNumber+1);
        }
        if (action == "subtract1"){
            HttpContext.Session.SetInt32("number",(int)currentNumber-1);
        }
        if (action == "multiply2"){
            HttpContext.Session.SetInt32("number",(int)currentNumber*2);
        }
        if (action == "addRandom"){
            Random random = new Random();
            HttpContext.Session.SetInt32("number",(int)currentNumber+random.Next(1,11));
        }
        return RedirectToAction("Dashboard");
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
