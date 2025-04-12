using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mvc_Proje.Controllers
{
    public class HeadingController : Controller
    {
        private readonly HeadingManager _headingManager;
        private readonly CategoryManager _categoryManager;
        private readonly WriterManager _writerManager;

        public HeadingController()
        {
            _headingManager = new HeadingManager(new EfHeadingDal(), new EfCategoryDal(), new EfWriterDal());
            _categoryManager = new CategoryManager(new EfCategoryDal());
            _writerManager = new WriterManager(new EfWriterDal());
        }

        public IActionResult Index()
        {
            // Başlıkları listele
            var headingValues = _headingManager.GetHeadingList();
            return View(headingValues);
        }

        [HttpGet]
        public IActionResult AddHeading()
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

            List<SelectListItem> valueWriter = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Yazar Seçin",
                    Value = ""
                }
            };
            valueWriter.AddRange(_writerManager.GetList().Select(x => new SelectListItem
            {
                Text = x.WriterName + " " + x.WriterSurname,
                Value = x.WriterID.ToString()
            }));

            ViewBag.vlc = valueCategory; // Kategori listesini View'e gönder
            ViewBag.vlw = valueWriter;   // Yazar listesini View'e gönder
            return View();
        }

        [HttpPost]
        public IActionResult AddHeading(Heading heading)
        {
            // Başlık ekleme işlemi
            heading.HeadingDate = DateTime.Now;
            _headingManager.HeadingAdd(heading);
            return RedirectToAction("Index");
        }

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
            _headingManager.HeadingUpdate(p);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteHeading(int id)
        {
            var headingvalue = _headingManager.GetById(id);
            headingvalue.HeadingStatus = false;
            _headingManager.HeadingRemove(headingvalue);    
            return RedirectToAction("Index");
        }
        public ActionResult ReGetHeading(int id)
        {
            var headingvalue= _headingManager.GetById(id);
            headingvalue.HeadingStatus = true;  
            _headingManager.UpStatu(headingvalue);
            return RedirectToAction("Index");
        }
    }
}
