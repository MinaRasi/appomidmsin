using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmidApp.Models;
namespace OmidApp.Controllers;
[Authorize]
public class HomeController : Controller
{
  private readonly IQuestion db;
    public HomeController(IQuestion _db)
    {
      db = _db;
    }

  
    public IActionResult home()
    {
      //TODO: Implement Realistic Implementation
      return View();
    }
  public IActionResult quiz()
  {
    int FinalNumber=db.GetFinalQuestion(User.Identity.GetId());
    return Redirect("/question/question?id="+FinalNumber+"&&UserAnswer=first");
    
 
  }
  public IActionResult ViewRate()
  {
    var Result=db.ShowResult().OrderByDescending(x=>x.Rate).ToList();
    return View(Result);
  }
  public IActionResult aboutus()
  {
    //TODO: Implement Realistic Implementation
    return View();
  }

}
