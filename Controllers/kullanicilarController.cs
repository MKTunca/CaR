using CarMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarMVC.Controllers
{
    public class kullanicilarController : Controller
    {
        RentACarEntities baglan = new RentACarEntities();

        public ActionResult liste()
        {
            try
            {
                ViewBag.uyarTXT = Session["uyari"];
            }
            catch
            { Session["uyari"] = ""; }


            var kullaniciListe = baglan.User.ToList();
            return View(kullaniciListe);
        }
        public JsonResult sil(int id)
        {
            var silinecekData = baglan.User.Find(id);

            baglan.User.Remove(silinecekData);

            baglan.SaveChanges();

            return Json("Silme işlemi tamamlandı.", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(User yonetici)
        {
            if ((yonetici.Email != null) && (yonetici.Sifre != null))
            {
                var eskiEmail = baglan.User.FirstOrDefault(m => m.Email == yonetici.Email);
               

                if (eskiEmail == null)
                {
                    
                    baglan.User.Add(yonetici);
                    baglan.SaveChanges();

                    Session["uyari"] = "Bilgileriniz kaydedildi.";
                }
                else
                { Session["uyari"] = "E-mail zaten kayıtlı."; }
            }
            else
            {
                Session["uyari"] = "E-mail ve şifre alanları zorunlu.";
            }

            return RedirectToAction("liste");

            
        }
        [HttpPost]
        public JsonResult degistir(User yonetim)
        {
            
            
            
                var degisim = baglan.User.Find(yonetim.ID);
           

                degisim.Email = yonetim.Email;
                degisim.Sifre = yonetim.Sifre;
                degisim.Tel = yonetim.Tel;
                degisim.AdSoyad = yonetim.AdSoyad;
            

            
               baglan.SaveChanges();
               

            return Json("Bilgileriniz güncellendi", JsonRequestBehavior.AllowGet);
        }
    }
}