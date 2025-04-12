using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EntityLayer.Concrete;
using Mvc_Proje.ViewModels; // Session için gerekli

namespace Mvc_Proje.Controllers
{
    public class WriterContentController : Controller
    {
        ContentManager _contentManager;
        IHttpContextAccessor _httpContextAccessor;

        public WriterContentController(IHttpContextAccessor httpContextAccessor)
        {
          
            _contentManager = new ContentManager(new EfContentDal(), new EfWriterDal(), new EfHeadingDal());
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult MyContent()
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

                var contentvalues = _contentManager.GetListByWriter(writerinfo);
                return View(contentvalues);
            }
        }

        [HttpGet]
        public IActionResult AddContent(int id)
        {
            var heading = _contentManager.GetByIDForContent(id); // HeadingManager ile başlık bilgisi alınıyor.
            if (heading == null)
            {
                return NotFound(); // Eğer başlık bulunamazsa 404 döner.
            }
            var model = new ContentViewModel
            {
                Heading = heading,
                Content = new Content { HeadingID = id } // Yeni bir Content nesnesi oluştur
            };
            return View(model); // Content nesnesi View'e gönderilir.
        }

        [HttpPost]
        public IActionResult AddContent(Content content)
        {
            var writerEmail = _httpContextAccessor.HttpContext?.Session.GetString("WriterEmail");

            if (writerEmail == null)
            {
                return RedirectToAction("WriterLogin", "Login"); // Kullanıcı oturum açmamışsa login'e yönlendir.
            }

            using (var context = new Context())
            {
                var writerInfo = context.Writers
                    .Where(x => x.WriterEmail == writerEmail)
                    .Select(y => y.WriterID)
                    .FirstOrDefault(); // Oturumdaki yazar bilgisi alınır.

                content.WriterID = writerInfo; // Yazar ID'si eklenir.
                content.ContentStatus = true; // İçeriğin durumu aktif olarak belirlenir.
                content.ContentDate = DateTime.Now; // İçerik tarihi atanır.

                _contentManager.ContentAdd(content); // İçerik ContentManager ile eklenir.
                return RedirectToAction("MyContent"); // MyContent sayfasına yönlendirilir.
            }

        }
    }
}