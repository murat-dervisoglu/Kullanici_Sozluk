using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Mvc_Proje.Controllers
{
    public class CommunicationController : Controller
    {
        CommunicationManager _communicationManager = new CommunicationManager(new EfCommunicationDal());
        CommunicationValidator _communicationValidator = new CommunicationValidator();
        public IActionResult Index()
        {
            var communicationValues = _communicationManager.GetList();
            return View(communicationValues);
        }
        public ActionResult GetComDetails(int id)
        {
            var comValues = _communicationManager.GetByID(id);
            return View(comValues);
        }
    }
}
