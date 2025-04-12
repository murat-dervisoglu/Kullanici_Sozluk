using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc_Proje.ViewModels;

namespace Mvc_Proje.Controllers
{
    [AllowAnonymous]
    public class DefaultHeadingController : Controller
    {
        HeadingManager _headingmanager = new HeadingManager(new EfHeadingDal());

        ContentManager _contentmanager = new ContentManager(new EfContentDal(), new EfWriterDal(), new EfHeadingDal());

        public ActionResult Headings()
        {
            var headinglist = _headingmanager.getlist();
            var contentlist = _contentmanager.GetList();

            ViewData["Headings"] = headinglist;

            // ViewModel'i doldur
            var viewModel = new HeadingContentViewModel
            {
                Headings = headinglist,
                Contents = contentlist
            };

            return View(viewModel);
        }
        public ActionResult ContentByHeading(int id)
        {          
            var headinglist = _headingmanager.getlist();
            var contentlist = _contentmanager.GetListByHeadingID(id);

            ViewData["Headings"] = headinglist;

            return View(contentlist);
        }
    }
}
