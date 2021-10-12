using CarMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarMVC.Controllers
{
    public class AdminController : Controller
    {
        RentACarEntities baglan = new RentACarEntities();
        public ActionResult login()
        {
            if (User.Identity.Name != "")
            { FormsAuthentication.SignOut(); }

            return View();
        }
        [HttpPost]
        public ActionResult login(yetkin yt)
        {
            if (yt.email != null)
            {
                if (yt.sifre != null)
                {
                    var bilgi = baglan.yetkin.FirstOrDefault(m => m.email == yt.email && m.sifre == yt.sifre);

                    if (bilgi != null)
                    {
                        FormsAuthentication.RedirectFromLoginPage(bilgi.ID.ToString(), false);
                        return RedirectToAction("aliste", "araclar");
                    }
                    else
                    {
                        ViewBag.uyari = "Bilgilerinizi kontrol ediniz.";
                    }
                }
                else
                {
                    ViewBag.uyari = "Şifrenizi yazınız.";
                }
            }
            else
            {
                ViewBag.uyari = "E-mail adresinizi yazınız.";
            }

            return View();
        }
    }
}