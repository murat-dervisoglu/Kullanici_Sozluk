using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Mvc_Proje.Controllers
{
    public class WriterMessageController : Controller
    {
        MessageManager _messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();
        IHttpContextAccessor _httpContextAccessor;

        public WriterMessageController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //----------------------- GELEN KUTUSU -------------------------------------------
        
        public ActionResult Inbox(string p)
        {
            var writerEmail = _httpContextAccessor.HttpContext?.Session.GetString("WriterEmail");

            if (writerEmail == null)
            {
                return RedirectToAction("WriterLogin", "Login");
            }

            var messagelist = _messageManager.GetMessagesTake(writerEmail);
            return View(messagelist);
        }

        //-----------------------MESAJLAR PARTİAL VİEW-------------------------------------------

        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }

        //------------------------gİDEN MESAJLAR----------------------------------------------------

        public ActionResult Sendbox(string p)
        {
            var writerEmail = _httpContextAccessor.HttpContext?.Session.GetString("WriterEmail");

            if (writerEmail == null)
            {
                return RedirectToAction("WriterLogin", "Login");
            }

            var messagelist = _messageManager.GetMessagesSend(writerEmail);
            return View(messagelist);
        }

        //---------------------GELEN MESAJ DETAYLARI---------------------------------------------

        public ActionResult GetInboxDetail(int id)
        {
            var values = _messageManager.GetByID(id);
            return View(values);
        }

        //--------------------GİDEN MESAJ DETAYLARI----------------------------------------------

        public ActionResult GetSendboxDetails(int id)
        {
            var values = _messageManager.GetByID(id);
            return View(values);
        }

        //---------------------YENİ MESAJ EKLE---------------------------------------------

        [HttpGet]
        public ActionResult AddMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMessage(Message p)
        {
            var writerEmail = _httpContextAccessor.HttpContext?.Session.GetString("WriterEmail");

            if (writerEmail == null)
            {
                return RedirectToAction("WriterLogin", "Login");
            }

            ValidationResult validationResult = messagevalidator.Validate(p);
            if (validationResult.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                _messageManager.MessageAdd(p);
                return RedirectToAction("Sendbox");
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
