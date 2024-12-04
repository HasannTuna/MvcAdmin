﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProje.Models.Entity;

namespace MvcProje.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBLMUSTERILER select d;
            if(!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());  
           // var degerler=db.TBLMUSTERILER.ToList(); 
           // return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMüsteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMüsteri(TBLMUSTERILER p1)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniMüsteri");
            }
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult SIL(int id)
        {
            var dgr=db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(dgr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mstr = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir",mstr);
        }

        public ActionResult Guncelle(TBLMUSTERILER p1)
        {
            var musteri = db.TBLMUSTERILER.Find(p1.MUSTERIID);
            musteri.MUSTERIAD = p1.MUSTERIAD;
            musteri.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}