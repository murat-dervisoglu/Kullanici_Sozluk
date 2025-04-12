using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mvc_Proje.Controllers
{
    
    public class MessageController : Controller
    {
        MessageManager _messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();


        //---------------------GELEN MESAJLAR---------------------------------------------

        public ActionResult Inbox(string p)
        {
            var messagelist = _messageManager.GetMessagesTake(p);
            return View(messagelist);
        }

        //---------------------GİDEN MESAJLAR--------------------------------------------

        public ActionResult Sendbox(string p)
        {
            var messagelist = _messageManager.GetMessagesSend(p);
            return View(messagelist);
        }

        //---------------------GELEN MESAJ DETAYLARI---------------------------------------------

        public ActionResult GetInboxDetail(int id)
        {
            var values = _messageManager.GetByID(id);
            return View(values);
        }
        //---------------------GİDEN MESAJ DETAYLARI---------------------------------------------
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
            ValidationResult validationResult = messagevalidator.Validate(p);
            if (validationResult.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.SenderMail = "admin@gmail.com";
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
