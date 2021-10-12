using CarMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KategoriMVC.Controllers
{
    public class kategorilerController : Controller
    {
        RentACarEntities baglan = new RentACarEntities();
        public ActionResult Index()
        {
            ViewBag.mesaj = "";
            var model = baglan.AKategori.ToList();

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string katAdi = fc["aKatTanim"];

            if ((katAdi != "") && (katAdi != null))
            {
                baglan.AKategori.Add(new AKategori
                {
                    akat = katAdi
                });

                baglan.SaveChanges();
            }
            else
            { ViewBag.mesaj = "Kategori adı yazınız."; }

            var model = baglan.AKategori.ToList();
            return View(model);
        }
        [HttpPost]
        public JsonResult aDegistir(AKategori a)
        {
            var degisim = baglan.AKategori.Find(a.ID);
            degisim.akat = a.akat;
            baglan.SaveChanges();
            return Json("Ana kategori güncellendi", JsonRequestBehavior.AllowGet);
        }
        public JsonResult aSil(int id)
        {
            var bkatlisteler = baglan.Bkategori.Where(m => m.akat_id == id).ToList();
            if (bkatlisteler != null)
            {
                foreach (var item in bkatlisteler)
                {
                    var cliste = baglan.Ckategori.Where(m => m.bkat_id == item.ID).ToList();

                    baglan.Ckategori.RemoveRange(cliste);
                    baglan.SaveChanges();
                }

                baglan.Bkategori.RemoveRange(bkatlisteler);

            }
            baglan.SaveChanges();
            var silinecek = baglan.AKategori.Find(id);
            baglan.AKategori.Remove(silinecek);
            baglan.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult kategoriB(int id)
        {
            ViewBag.mesaj = "";
            var model = baglan.Bkategori.Where(m => m.akat_id == id).ToList();

            ViewBag.akatID = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult kategoriB(int id, FormCollection fc)
        {
            string katAdi = fc["bKatTanim"];

            if ((katAdi != "") && (katAdi != null))
            {
                baglan.Bkategori.Add(new Bkategori
                {
                    bkat = katAdi,
                    akat_id = id
                });

                baglan.SaveChanges();
            }
            else
            { ViewBag.mesaj = "Kategori adı yazınız."; }

            ViewBag.akatID = id;

            var model = baglan.Bkategori.Where(m => m.akat_id == id).ToList();
            return View(model);
        }
        [HttpPost]
        public JsonResult bDegistir(Bkategori b)
        {
            var degisim = baglan.Bkategori.Find(b.ID);
            degisim.bkat = b.bkat;
            baglan.SaveChanges();
            return Json("Alt kategori güncellendi", JsonRequestBehavior.AllowGet);
        }
        public JsonResult bSil(int id)
        {
            var clisteler = baglan.Ckategori.Where(m => m.bkat_id == id).ToList();
            if (clisteler != null)
            {


                baglan.Ckategori.RemoveRange(clisteler);

            }
            baglan.SaveChanges();

            var sil = baglan.Bkategori.Find(id);

            baglan.Bkategori.Remove(sil);

            baglan.SaveChanges();

            return Json("Ok", JsonRequestBehavior.AllowGet);

        }

        public ActionResult kategoriC(int id)
        {
            ViewBag.mesaj = "";
            var model = baglan.Ckategori.Where(m => m.bkat_id == id).ToList();

            ViewBag.bkatID = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult kategoriC(int id, FormCollection fc)
        {
            string katAdi = fc["cKatTanim"];

            if ((katAdi != "") && (katAdi != null))
            {
                baglan.Ckategori.Add(new Ckategori
                {
                    ckat = katAdi,
                    bkat_id = id
                });

                baglan.SaveChanges();
            }
            else
            { ViewBag.mesaj = "Kategori adı yazınız."; }

            ViewBag.bkatID = id;

            var model = baglan.Ckategori.Where(m => m.bkat_id == id).ToList();
            return View(model);
        }
        [HttpPost]
        public JsonResult cDegistir(Ckategori c)
        {
            var degisim = baglan.Ckategori.Find(c.ID);
            degisim.ckat = c.ckat;
            baglan.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public JsonResult cSil(int id)
        {
            var sil = baglan.Ckategori.Find(id);

            baglan.Ckategori.Remove(sil);

            baglan.SaveChanges();

            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
    }
}