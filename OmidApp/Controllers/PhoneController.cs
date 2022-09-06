using Kavenegar;
using Microsoft.AspNetCore.Mvc;

public class PhoneController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Check(string phone)
    {
        Random rnd = new Random();
        int code = rnd.Next(1000,9999);

        HttpContext.Session.SetInt32("code", code);
        var api = new KavenegarApi("3871353043697339486A70384F544A4A574C74612B51432F4C7A4B305076645457396F5267456F7A5A34383D");
        var result = api.VerifyLookup(phone, code.ToString(),"demo");
        return View();
    }
    public IActionResult Baresi(string code)
    {
        if (code == HttpContext.Session.GetInt32("code").ToString())
        {
            return RedirectToAction("home","home");
        }
        else
        {
            return RedirectToAction("check");
        }


    }
}