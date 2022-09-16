using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmidApp.Models;

[Area("Admin")]
public class HomeController : Controller
{
  public IActionResult Index()
    {
        return View();
    }
     public IActionResult login()
    {
        return View();
    }
     public IActionResult log(int Password, string email)
    {
        if (Password == 1234 && email == "admin")
        {
               var claims = new List<Claim>() 
               {
               new Claim (ClaimTypes.NameIdentifier,"admin"),
               new Claim (ClaimTypes.Name, "admin")
               };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddYears(1),
                    IsPersistent = true
                };
                HttpContext.SignInAsync(principal, properties);
                return RedirectToAction("index");
        }
        else
        {
            
             TempData["error"] = "Error";
             return RedirectToAction("login");
        }
    }
    public IActionResult Participantsscore()
    {
      //TODO: Implement Realistic Implementation
      return View();
    }
    public IActionResult info()
    {
      //TODO: Implement Realistic Implementation
      return View();
    }
    public IActionResult agent()
    {
      //TODO: Implement Realistic Implementation
      return View();
    }
    public IActionResult paystatus()
    {
      //TODO: Implement Realistic Implementation
      return View();
    }
    public IActionResult setting()
    {
      //TODO: Implement Realistic Implementation
      return View();
    }
    public IActionResult bag()
    {
      //TODO: Implement Realistic Implementation
      return View();
    }
    

}


