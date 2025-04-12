using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Mvc_Proje.Controllers
{
    public class WriterController : Controller
    {
        WriterManager _writerManager = new WriterManager(new EfWriterDal());
        WriterValidator writervalidator = new WriterValidator();
        public IActionResult Index()
        {
            var writerValues = _writerManager.GetList();
            return View(writerValues);
        }
        [HttpGet]
        public IActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddWriter(Writer p)
        {
            ValidationResult validationResult = writervalidator.Validate(p);
            if (validationResult.IsValid)
            {
                _writerManager.WriterAdd(p);
                return RedirectToAction("Index");
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
        [HttpGet]   
        public ActionResult EditWriter(int id)
        {
            var writervalue =_writerManager.GetById(id);
            return View(writervalue);
        }
        [HttpPost]   
        public ActionResult EditWriter(Writer p)
        {
            ValidationResult validationResult = writervalidator.Validate(p);
            if (validationResult.IsValid)
            {
                _writerManager.WriterUpdate(p);
                return RedirectToAction("Index");
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
    }
}
