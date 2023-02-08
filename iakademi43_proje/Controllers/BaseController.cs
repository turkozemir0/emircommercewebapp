using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iakademi43_proje.Models;

namespace iakademi43_proje.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base

        //constructor
        public BaseController()
        {
            iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities();
            ViewBag.kategoriListesi = db.tbl_Categories.ToList();
            ViewBag.markaListesi = db.tbl_Suppliers.ToList();
            ViewBag.sayfadakiurunsayisi = db.tbl_Settings.FirstOrDefault(s => s.ID == 1).sayfadakiurunsayisi;
            ViewBag.telefon = db.tbl_Settings.FirstOrDefault(s => s.ID == 1).telefon;
            ViewBag.anasayfadakiurunsayisi = db.tbl_Settings.FirstOrDefault(s => s.ID == 1).anasayfadakiurunsayisi;
        }
        


    }
}