using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OmidApp.Models;
namespace OmidApp.Controllers;

public class UserController : Controller
{
    private readonly IUser db;
    public UserController(IUser _db)
    {
        db = _db;
    }
    public IActionResult Profile()
    {
        var id=User.Identity.GetId();
        var user=db.ShowUser(Convert.ToInt32(id));
        return View(user);
    }
    public IActionResult EditProfile(VmUser u)
    {
        db.EditUserProfile(u);
        return RedirectToAction("home","home");
    }
    

}


