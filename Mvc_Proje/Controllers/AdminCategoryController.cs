using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mvc_Proje.Controllers
{
    public class AdminCategoryController : Controller
    {
        CategoryManager _categoryManager = new CategoryManager(new EfCategoryDal());
        [Authorize ]
        public IActionResult Index()
        {
            var categoryvalues = _categoryManager.GetList();
            return View(categoryvalues);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category p)
        {
            CategoryValidator validator = new CategoryValidator();
            ValidationResult validationResult = validator.Validate(p);
            if (validationResult.IsValid)
            {
                _categoryManager.CategoryAdd(p);
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
        public ActionResult DeleteCategory(int id)
        {
            var categoryvalue = _categoryManager.GetById(id);   
            _categoryManager.CategoryDelete(categoryvalue);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var categoryvalue = _categoryManager.GetById(id);
            return View(categoryvalue);
        }
        [HttpPost]
        public ActionResult EditCategory(int id, Category p)
        {
            _categoryManager.CategoryUpdate(p);
            return RedirectToAction("Index");
        }

    }
}
