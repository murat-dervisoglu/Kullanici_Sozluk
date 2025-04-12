using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Mvc_Proje.ViewModels;

namespace Mvc_Proje.Controllers
{

    // BÜTÜN İŞ CONTROLLER VE MANAGER KISMINDA BİTİYOR

    public class ContentController : Controller
    {
        private readonly ContentManager _contentManager;
        HeadingManager _headingManager;

        public ContentController()
        {
            _contentManager = new ContentManager(new EfContentDal(), new EfWriterDal(),new EfHeadingDal());
            _headingManager = new HeadingManager (new  EfHeadingDal()); 
        }

        public IActionResult Index()
        {
            return View();
        }

      
        public ActionResult GetAllContent(string p="" )
        {
            var values = _contentManager.GetListForFind(p);   
            return View(values);
        }

        public ActionResult ContentByHeading(int id)
        {
            var contentvalues= _contentManager.GetListByHeadingID(id);
            return View(contentvalues);
        }
    }
}
