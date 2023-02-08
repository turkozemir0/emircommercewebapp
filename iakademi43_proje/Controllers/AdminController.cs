using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iakademi43_proje.Models;

namespace iakademi43_proje.Controllers
{
    public class AdminController : Controller
    {
        iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities();
        // GET: Admin
        public ActionResult AnaSayfa()
        {
            return View();
        }


        public ActionResult KategoriKaydet()
        {
            List<tbl_Categories> catlist = db.tbl_Categories.ToList();
            ViewData["catlist"] = catlist.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString() });

            return View();
        }

        [HttpPost]
        public ActionResult KategoriKaydet(tbl_Categories cat)
        {
           bool sonuc =   cls_Categories.kategori_ismi_varmi_kontrolu(cat.CategoryName.ToLower());

            if (sonuc == false)
            {
                //kaydet
                if (cat.CategoryID == 0)
                {
                    cat.ParentID = 0;
                }
                else
                {
                    cat.ParentID = cat.CategoryID;
                }

                //turner if
                // cat.ParentID =  cat.CategoryID == 0 ?  0 : cat.CategoryID;

                cat.Active = true;
                db.tbl_Categories.Add(cat);
                db.SaveChanges();
            }
            else
            {
                Session["Mesaj"] = "Bu kategori zaten kayıtlı.";
            }
           
            return RedirectToAction("KategoriKaydet"); //[httpGet]
        //  return View(); //[HttpPost]
        }




        public ActionResult MarkaKaydet()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MarkaKaydet(tbl_Suppliers sup,HttpPostedFileBase fileuploader)
        {
            sup.PhotoPath = fileuploader.FileName;
            sup.Active = true;
            db.tbl_Suppliers.Add(sup);
            db.SaveChanges();
            return View();
        }



        public ActionResult UrunKaydet()
        {
            List<tbl_Categories> catlist = db.tbl_Categories.ToList();
            ViewData["catlist"] = catlist.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString() });

            List<tbl_Suppliers> suplist = db.tbl_Suppliers.ToList();
            ViewData["suplist"] = suplist.Select(c => new SelectListItem { Text = c.BrandName, Value = c.SupplierID.ToString() });

            List<tbl_Status> statuslist = db.tbl_Status.ToList();
            ViewData["statuslist"] = statuslist.Select(c => new SelectListItem { Text = c.adi, Value = c.statusID.ToString() });

            return View();
        }

        [HttpPost]
        public ActionResult UrunKaydet(tbl_Products prd, HttpPostedFileBase fileuploader)
        {
            if (prd.Discount == null)
            {
                prd.Discount = 0;
            }
            if (prd.Keywords == null)
            {
                prd.Keywords = "";
            }
            if (prd.Notes == null)
            {
                prd.Notes = "";
            }
            prd.AddDate = DateTime.Now;
            prd.OneCikanlar = 0;
            prd.CokSatanlar = 0;
            prd.PhotoPath = fileuploader.FileName;
            prd.Active = true;
            db.tbl_Products.Add(prd);
            db.SaveChanges();
            return RedirectToAction("UrunKaydet"); //[httpget]
        }


        public ActionResult AyarlarKaydet()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AyarlarKaydet(tbl_Settings set)
        {
            bool sonuc = cls_Settings.ayarlar_kaydet(set);

            if (sonuc == true)
            {
                Session["Mesaj"] = "Ayarlar Kaydedildi.";
                return RedirectToAction("AnaSayfa"); 
            }
            else
            {
                Session["Mesaj"] = "HATA !!! Ayarlar Kaydedilemedi.";
                return View();
            }
        }

        public ActionResult UrunListele()
        {
            List<vw_urunler> urn = db.vw_urunler.OrderByDescending(u => u.ProductID).ToList();
            return View(urn);
        }

        [HttpGet]
        public ActionResult UrunGuncelle(int id)
        {
            List<tbl_Categories> catlist = db.tbl_Categories.ToList();
            ViewData["catlist"] = catlist.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString() });

            List<tbl_Suppliers> suplist = db.tbl_Suppliers.ToList();
            ViewData["suplist"] = suplist.Select(c => new SelectListItem { Text = c.BrandName, Value = c.SupplierID.ToString() });

            List<tbl_Status> statuslist = db.tbl_Status.ToList();
            ViewData["statuslist"] = statuslist.Select(c => new SelectListItem { Text = c.adi, Value = c.statusID.ToString() });

            vw_urunler urn = db.vw_urunler.FirstOrDefault(u => u.ProductID == id);
            return View(urn);
        }

        [HttpPost]
        public ActionResult UrunGuncelle(tbl_Products prd,HttpPostedFileBase fileuploader)
        {
            vw_urunler urn = db.vw_urunler.FirstOrDefault(p => p.ProductID == prd.ProductID);
            prd.AddDate = urn.AddDate;
            prd.CokSatanlar = urn.CokSatanlar;
            prd.OneCikanlar = urn.OneCikanlar;
            if (fileuploader != null)
            {
                prd.PhotoPath = fileuploader.FileName;
            }
            else
            {
                prd.PhotoPath = urn.PhotoPath;
            }

            db.Entry(prd).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("UrunListele"); //[HttpGet]
        }


        public ActionResult urunSil(int id)
        {
            tbl_Products prd = db.tbl_Products.FirstOrDefault(p => p.ProductID == id);
            prd.Active = false;
            db.Entry(prd).State = System.Data.Entity.EntityState.Modified;
            /* db.tbl_Products.Remove(prd);*/
            db.SaveChanges();
            return RedirectToAction("UrunListele");
        }

    }
}