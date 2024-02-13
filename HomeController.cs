using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Listele()
        {
            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            //ViewBag.isler = db.islers;
            ViewBag.sayfa = "acikIsler";
            ViewBag.isler = db.islers.Where(o => o.durum == "1").ToList();
            return View();
        }

        public ActionResult KapaliIsListele()
        {
            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            //ViewBag.isler = db.islers;
            ViewBag.sayfa = "kapaliIsler";
            ViewBag.isler = db.islers.Where(o => o.durum == "0").ToList();
            return View();
        }

        public ActionResult Kaydet(String txtIsinAdi)
        {
            string is_adi = txtIsinAdi.ToString();
            DateTime olusturuldugu_tarih = DateTime.Now;
            string durum = "1";
            
            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            var yeniIs = new isler
            {
                is_adi = is_adi,
                durum = durum,
                olusturuldugu_tarih= olusturuldugu_tarih

            };
            db.islers.Add(yeniIs);
            db.SaveChanges();         

           
            return RedirectToAction("Listele");
        }
       
        public ActionResult Sil(int id)
        {
            ToDoEntitiesConnectionStringDB db =  new ToDoEntitiesConnectionStringDB();
            isler silinecek = db.islers.FirstOrDefault(x => x.id == id);
            db.islers.Remove(silinecek);
            db.SaveChanges();
            return RedirectToAction("Listele");
        }

        public ActionResult IsKapat(int id)
        {
            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            isler kapatilacakIs = db.islers.FirstOrDefault(x => x.id == id);
            kapatilacakIs.tamamlandigi_tarih = DateTime.Now;
            kapatilacakIs.durum = "0";
            db.SaveChanges();
            return RedirectToAction("Listele");

        }

        public ActionResult IsiYenidenAc(int id)
        {
            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            isler acilacakIs = db.islers.FirstOrDefault(x => x.id == id);
            acilacakIs.tamamlandigi_tarih = null;
            acilacakIs.durum = "1";
            db.SaveChanges();
            return RedirectToAction("KapaliIsListele");

        }
    }
}