using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace Mvc_Proje.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class LoginController : Controller
    {
        WriterLoginManager _writerLoginManager = new WriterLoginManager(new EfWriterDal());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Admin p)
        {
            Context c = new Context();
            var adminuser = c.Admins.FirstOrDefault(x => x.AdminName == p.AdminName && x.AdminPassword == p.AdminPassword);

            if (adminuser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, adminuser.AdminName)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı!";
                return View();
            }
        }

        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> WriterLogin(Writer p)
        {
            //Context c = new Context();
            //var writeruser = c.Writers.FirstOrDefault(x => x.WriterEmail == p.WriterEmail && x.WriterPassword == p.WriterPassword);

            var writeruser = _writerLoginManager.GetWriter(p.WriterEmail, p.WriterPassword);

            if (writeruser != null)
            {
                // Session'a yazarın email bilgisini kaydet
                HttpContext.Session.SetString("WriterEmail", writeruser.WriterEmail);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, writeruser.WriterEmail)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("MyContent", "WriterContent");
            }
            else
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı!";
                return View();
            }
        }
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Headings","DefaultHeading");
        }
    }
}

