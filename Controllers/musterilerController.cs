using CarMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarMVC.Controllers
{
    public class musterilerController : Controller
    {
        RentACarEntities baglan = new RentACarEntities();
        public ActionResult mliste()
        {
            try
            {
                ViewBag.uyarTXT = Session["uyari"];
            }
            catch
            { Session["uyari"] = ""; }

            List<object> drm = new List<object>();

            drm.Add(new SelectListItem { Text = "Aktif", Value = "Aktif" });
            drm.Add(new SelectListItem { Text = "Pasif", Value = "Pasif" });

            ViewBag.durumlar = drm;

            var musteriliste = baglan.customer.ToList();

            return View(musteriliste);
        }
        public ActionResult mEkle()
        {
            ViewBag.maxyil = DateTime.Now.Year-18;
            
            return View();
        }
        [HttpPost]
        public ActionResult mEkle(customer mu)
        {
            if ((mu.email != null) && (mu.sifre != null))
            {
                var eskiemail = baglan.customer.FirstOrDefault(m => m.email == mu.email);


                if (eskiemail == null)
                {

                    baglan.customer.Add(mu);
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

            return RedirectToAction("mliste");
        }
        [HttpPost]
        public JsonResult degistir(customer deg)
        {
            var degis = baglan.customer.Find(deg.ID);
            degis.adsoyad = deg.adsoyad;
            degis.email = deg.email;
            degis.sifre = deg.sifre;
            degis.telno = deg.telno;
            degis.etipi = deg.etipi;
            degis.dyil = deg.dyil;
            degis.il = deg.il;
            degis.ilce = deg.ilce;
            degis.adres = deg.adres;
            if (deg.durum != null)
            { degis.durum = deg.durum; }

            baglan.SaveChanges();
            return Json("Bilgileriniz güncellendi", JsonRequestBehavior.AllowGet);
        }
        public JsonResult sil(int id)
        {
            var silinecek = baglan.customer.Find(id);

            baglan.customer.Remove(silinecek);

            baglan.SaveChanges();

            return Json("Silme işlemi tamamlandı.", JsonRequestBehavior.AllowGet);
        }
    }
}