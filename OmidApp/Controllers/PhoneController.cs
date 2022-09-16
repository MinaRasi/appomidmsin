using System.Security.Claims;
using Kavenegar;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
public class PhoneController : Controller
{
    private readonly IQuestion db;
    private readonly IUser dbuser;

    public PhoneController(IQuestion db,IUser _dbuser)
    {
        this.db = db;
        dbuser=_dbuser;
    }
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Check(string phone)
    {
        Random rnd = new Random();
        int code = rnd.Next(1000, 9999);
        HttpContext.Session.SetString("phone", phone);
        var api = new KavenegarApi("3871353043697339486A70384F544A4A574C74612B51432F4C7A4B305076645457396F5267456F7A5A34383D");
        var result = api.VerifyLookup(phone, code.ToString(), "demo");
        db.Savecode(phone, code);
        return View();
    }
    public IActionResult Baresi(int code)
    {
        var phone = HttpContext.Session.GetString("phone").ToString();
        
        var quser = db.check(phone, code);
        if (quser != null)
        {
            // auttocation///////////////////////////////////////////

            ClaimsIdentity identity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name ,quser.FirstAndLastName ) ,
                            new Claim(ClaimTypes.NameIdentifier,quser.Id.ToString() )
                        }, CookieAuthenticationDefaults.AuthenticationScheme);

            var princpal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties

            {
                ExpiresUtc = DateTime.UtcNow.AddYears(1),
                IsPersistent = true
            };
            HttpContext.SignInAsync(princpal, properties);
            
            return RedirectToAction("home", "home");

            //  var claims = new List<Claim>() 
            //    {
            //       new Claim (ClaimTypes.NameIdentifier,quser.Id.ToString()),
            //       new Claim (ClaimTypes.Name, quser.FirstAndLastName)
            //    };
            //     var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //     var principal = new ClaimsPrincipal(identity);
            //     var properties = new AuthenticationProperties
            //     {
            //         ExpiresUtc = DateTime.UtcNow.AddYears(1),
            //         IsPersistent = true
            //     };


        }
        else
        {
            return RedirectToAction("login");
        }


    }

    public IActionResult logout()
    {
       
     HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
       return RedirectToAction("login");
    }
}