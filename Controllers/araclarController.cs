using CarMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarMVC.Controllers
{
    [_AdminControl]
    public class araclarController : Controller
    {
        RentACarEntities baglan = new RentACarEntities();
        public ActionResult aliste()
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

            var aracliste = baglan.ARAC.ToList();
            return View(aracliste);
        }
        public string fotoislem(HttpPostedFileBase f)
        {
            string sonuc = "";

            int fotoMB = f.ContentLength;
            string fotoTanim = f.FileName;
            string mimeTip = f.ContentType;
            string uzanti = fotoTanim.Substring(fotoTanim.LastIndexOf("."));

            if (f.ContentLength < 1000000)
            {
                if (uzanti.IndexOf("js") == -1)
                {
                    f.SaveAs(Server.MapPath("~/imajlar/") + fotoTanim);
                    sonuc = fotoTanim;
                }
                else
                {
                    sonuc = "Hata : Dosya geçersiz.";
                }
            }
            else
            {
                sonuc = "Hata : Dosya 1 mb daha büyük.";
            }

            return sonuc;
        }
        public ActionResult AEkle()
        {
            var alist = baglan.AKategori.ToList();
            ViewBag.AnaKategori = alist;
            Int32 akatID = alist[0].ID;
            ViewBag.AltKategori = baglan.Bkategori.Where(m => m.akat_id == akatID).ToList();
            ViewBag.maxYil = DateTime.Now.Year;
            return View();
        }
        [HttpPost]
        public ActionResult AEkle(ARAC ar,HttpPostedFileBase f)
        {
            if ((ar.marka != null) && (ar.model != null))
            {
                var eskiplaka = baglan.ARAC.FirstOrDefault(m => m.plaka == ar.plaka);
                ar.imaj = "";

                if (eskiplaka == null)
                {
                    if (f != null)
                    {
                        string sonuc = fotoislem(f);
                        if (sonuc.IndexOf("Hata : ") == -1)
                        {
                            ar.imaj = "~/imajlar/" + sonuc;
                        }
                    }

                    ar.durum = "Aktif";
                    baglan.ARAC.Add(ar);
                    baglan.SaveChanges();

                    Session["uyari"] = "Bilgileriniz kaydedildi.";
                }
                else
                { Session["uyari"] = "Plaka zaten kayıtlı."; }
            }
            else
            {
                Session["uyari"] = "Marka ve model alanları zorunlu.";
            }

            return RedirectToAction("aliste");
        }
        [HttpPost]
        public JsonResult degistir(ARAC d)
        {
            string cevap = "";
            var degisim = baglan.ARAC.Find(d.ID);
            if (User.Identity.Name != "")
            {
                
                degisim.marka = d.marka;
                degisim.model = d.model;
                degisim.plaka = d.plaka;
                degisim.yil = d.yil;
                degisim.ytip = d.ytip;
                degisim.vtip = d.vtip;
                degisim.maxyas = d.maxyas;
                if (d.durum != null)
                { degisim.durum = d.durum; }
                degisim.fiyat = d.fiyat;
                
                cevap = "Bilgileriniz güncellendi";
                baglan.SaveChanges();
            }
            else
            {
                cevap = "Yetkisiz giriş";
            }
            
            return Json(cevap, JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        public JsonResult imaj_degistir(int id)
        {
            var degisim = baglan.ARAC.Find(id);
            string sonuc = fotoislem(Request.Files[0]);

            if (sonuc.IndexOf("Hata : ") == -1)
            {
                degisim.imaj = "~/imajlar/" + sonuc;

                baglan.SaveChanges();
            }

            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
        public JsonResult sil(int id)
        {
            string sonuc = "";
            var silinecek = baglan.ARAC.Find(id);
            if (User.Identity.Name != "")
            {
                

                baglan.ARAC.Remove(silinecek);
                sonuc = "Silme işlemi tamamlandı";
                baglan.SaveChanges();
                
            }
            else
            {
                sonuc = "Yetkisiz giriş";
            }

            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
        public JsonResult altkategoriler(int id)
        {
            var altListe = baglan.Bkategori.Where(m => m.akat_id == id).ToList();

            return Json(altListe, JsonRequestBehavior.AllowGet);
        }

    }
}