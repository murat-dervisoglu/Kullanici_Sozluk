using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using X.PagedList;
using X.PagedList.Extensions;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;


namespace Mvc_Proje.Controllers
{
    public class WriterPanelController : Controller
    {
        CategoryManager _categoryManager;
        HeadingManager _headingManager;
        WriterManager _writerManager;
        IHttpContextAccessor _httpContextAccessor;
        WriterValidator writervalidator = new WriterValidator();

        public WriterPanelController(IHttpContextAccessor httpContextAccessor)
        {
            _headingManager = new HeadingManager(new EfHeadingDal(), new EfCategoryDal(), new EfWriterDal());
            _writerManager = new WriterManager(new EfWriterDal());
            _categoryManager = new CategoryManager(new EfCategoryDal());
            _httpContextAccessor = httpContextAccessor;
        }

        //-------------------KULLANICI PROFİLİ----------------------------------------
        [HttpGet]
        public IActionResult WriterProfile(int id)
        {
            var writerEmail = _httpContextAccessor.HttpContext?.Session.GetString("WriterEmail");

            if (writerEmail == null)
            {
                return RedirectToAction("WriterLogin", "Login");
            }

            using (var c = new Context())
            {
                var writerinfo = c.Writers
                    .Where(x => x.WriterEmail == writerEmail)
                    .Select(y => y.WriterID)
                    .FirstOrDefault();
                ViewBag.d = writerinfo;
                var writer = _writerManager.GetById(writerinfo);
                return View(writer);
            }
        }

        [HttpPost]
        public ActionResult WriterProfile(Writer p)
        {
            ValidationResult validationResult = writervalidator.Validate(p);
            if (validationResult.IsValid)
            {
                _writerManager.WriterUpdate(p);
                return RedirectToAction("MyHeading");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        //-------------------YAZDIĞIM BAŞLIKLAR----------------------------------------

        public IActionResult MyHeading()
        {
            var writerEmail = _httpContextAccessor.HttpContext?.Session.GetString("WriterEmail");

            if (writerEmail == null)
            {
                return RedirectToAction("WriterLogin", "Login");
            }

            using (var c = new Context())
            {
                var writerinfo = c.Writers
                    .Where(x => x.WriterEmail == writerEmail)
                    .Select(y => y.WriterID)
                    .FirstOrDefault();

                var contentvalues = _headingManager.GetHeadingListByWriter(writerinfo);
                return View(contentvalues);
            }
        }

        //-----------------------YENİ BAŞLIK------------------------------------

        [HttpGet]
        public IActionResult NewHeading()
        {
            List<SelectListItem> valueCategory = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Kategori Seçin",
                    Value = ""
                }
            };
            valueCategory.AddRange(_categoryManager.GetList().Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryID.ToString()
            }));
            ViewBag.vlc = valueCategory;
            return View();
        }

        [HttpPost]
        public IActionResult NewHeading(Heading heading)
        {
            var writerEmail = _httpContextAccessor.HttpContext?.Session.GetString("WriterEmail");

            if (writerEmail == null)
            {
                return RedirectToAction("WriterLogin", "Login");
            }

            using (var c = new Context())
            {
                var writerinfo = c.Writers
                    .Where(x => x.WriterEmail == writerEmail)
                    .Select(y => y.WriterID)
                    .FirstOrDefault();

                //var contentvalues = _headingManager.GetHeadingListByWriter(writerinfo);
                //return View(contentvalues);
           
                heading.HeadingDate = DateTime.Now;
                heading.WriterID = writerinfo;
                heading.HeadingStatus = true;
                heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                _headingManager.HeadingAdd(heading);
                return RedirectToAction("MyHeading");
             }
        }

        //---------------------BAŞLIK DÜZENLE--------------------------------------

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valueCategory = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Kategori Seçin",
                    Value = ""
                }
            };
            valueCategory.AddRange(_categoryManager.GetList().Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryID.ToString()
            }));

            ViewBag.vlc = valueCategory; // Kategori listesini View'e gönder
            var headingValue = _headingManager.GetById(id);
            return View(headingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            p.HeadingStatus = default;
            _headingManager.HeadingUpdate(p);
            return RedirectToAction("MyHeading");
        }

        //------------------BAŞLIK SİL-----------------------------------------

        public ActionResult DeleteHeading(int id)
        {
            var headingvalue = _headingManager.GetById(id);
            headingvalue.HeadingStatus = false;
            _headingManager.HeadingRemove(headingvalue);
            return RedirectToAction("MyHeading");
        }

        //------------------TÜM BAŞLIKLAR--------------------------------------

        public ActionResult AllHeading(int page = 1)
        {
            var headings = _headingManager.GetHeadingList().ToPagedList(page,4);
            return View(headings);
        }

    }
}
