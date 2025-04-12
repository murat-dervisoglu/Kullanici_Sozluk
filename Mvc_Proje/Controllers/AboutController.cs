using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Mvc_Proje.Controllers
{
    public class AboutController : Controller
    {
        AboutManager _aboutManager = new AboutManager(new EfAboutDal()); 
        public IActionResult Index()
        {
            var aboutvalues = _aboutManager.GetAbout();
            return View(aboutvalues);
        }

        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About p)
        {
            _aboutManager.AboutAdd(p);
            return RedirectToAction("Index");
        }

        public ActionResult AboutPartial()
        {
            return PartialView();
        }
    }
}
