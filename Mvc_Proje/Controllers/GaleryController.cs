using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Mvc_Proje.Controllers
{
    public class GaleryController : Controller
    {
        ImageFileManager _imageFileManager = new ImageFileManager(new EFImageFileDal());
        public IActionResult Index()
        {
            var files = _imageFileManager.GetList();
            return View(files);
        }
    }
}
