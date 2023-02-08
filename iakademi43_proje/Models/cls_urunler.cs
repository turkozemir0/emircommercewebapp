using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iakademi43_proje.Models
{
    public class cls_urunler
    {
        iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities();
        public static int sayfadakiurunsayisi = 0;
        public static int anasayfadakiurunsayisi = 0;

        public  List<vw_urunler> urunler_getir(string urunler,string hangisayfa,int sayfano)
        {
            List<vw_urunler> liste = null;

            /* ana sayfa icin start*/
            if (hangisayfa == "anasayfa")
            {
                if (urunler == "yeni")
                {
                    liste = db.vw_urunler.OrderByDescending(u => u.AddDate).Take(anasayfadakiurunsayisi).ToList();
                }
                else if (urunler == "slider")
                {
                    liste = db.vw_urunler.Where(u => u.statusID == 2).Take(anasayfadakiurunsayisi).ToList();
                }
                else if (urunler == "özel")
                {
                    liste = db.vw_urunler.Where(u => u.statusID == 3).Take(anasayfadakiurunsayisi).ToList();
                }
                else if (urunler == "indirim")
                {
                    liste = db.vw_urunler.Where(u => u.Discount > 0).OrderByDescending(u => u.Discount).Take(anasayfadakiurunsayisi).ToList();
                }
                else if (urunler == "onecikanlar")
                {
                    liste = db.vw_urunler.OrderByDescending(u => u.OneCikanlar).Take(anasayfadakiurunsayisi).ToList();
                }
                else if (urunler == "coksatanlar")
                {
                    liste = db.vw_urunler.OrderByDescending(u => u.CokSatanlar).Take(anasayfadakiurunsayisi).ToList();
                }
                else if (urunler == "firsat")
                {
                    liste = db.vw_urunler.Where(u => u.statusID == 4).Take(anasayfadakiurunsayisi).ToList();
                }
                else if (urunler == "yildiz")
                {
                    liste = db.vw_urunler.Where(u => u.statusID == 5).Take(anasayfadakiurunsayisi).ToList();
                }
                /* ana sayfa icin stop*/
            }
            else
            {
                //alt sayfa
                if (urunler == "yeni")
                {
                    if (sayfano == 0)
                    {
                        liste = db.vw_urunler.OrderByDescending(u => u.AddDate).Take(sayfadakiurunsayisi).ToList();
                    }
                    else
                    {
                        liste = db.vw_urunler.OrderByDescending(u => u.AddDate).Skip(sayfano * sayfadakiurunsayisi).Take(sayfadakiurunsayisi).ToList();
                    }
                }
                

                if (urunler == "özel")
                {
                    if (sayfano == 0)
                    {
                        liste = db.vw_urunler.Where(u => u.statusID == 3).OrderBy(u => u.ProductName).Take(sayfadakiurunsayisi).ToList();
                    }
                    else
                    {
                        liste = db.vw_urunler.Where(u => u.statusID == 3).OrderBy(u => u.ProductName).Skip(sayfano * sayfadakiurunsayisi).Take(sayfadakiurunsayisi).ToList();
                    }
                }


                if (urunler == "indirim")
                {
                    if (sayfano == 0)
                    {
                        liste = db.vw_urunler.Where(u => u.Discount > 0).OrderByDescending(u => u.Discount).Take(sayfadakiurunsayisi).ToList();
                    }
                    else
                    {
                        liste = db.vw_urunler.Where(u => u.Discount > 0).OrderByDescending(u => u.Discount).Skip(sayfano * sayfadakiurunsayisi).Take(sayfadakiurunsayisi).ToList();
                    }
                }



                if (urunler == "onecikanlar")
                {
                    if (sayfano == 0)
                    {
                        liste = db.vw_urunler.OrderByDescending(u => u.OneCikanlar).Take(sayfadakiurunsayisi).ToList();
                    }
                    else
                    {
                        liste = db.vw_urunler.OrderByDescending(u => u.OneCikanlar).Skip(sayfano * sayfadakiurunsayisi).Take(sayfadakiurunsayisi).ToList();
                    }
                }


            }
            return liste;
        }


        public vw_urunler urun_getir()
        {
            vw_urunler urn = db.vw_urunler.FirstOrDefault(u => u.statusID == 1);
            return urn;
        }


        // OneCikanlar kolonunu 1 arttırsın
        public static void OneCikanlar_kolonunu_arttırsın(int id)
        {
            using (iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities())
            {
                tbl_Products prd = db.tbl_Products.FirstOrDefault(p => p.ProductID == id);
                prd.OneCikanlar += 1;
                db.SaveChanges();
            }
        }

        public static vw_urunler urun_bilgisi_getir(int id)
        {
            using (iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities())
            {
                vw_urunler urn = db.vw_urunler.FirstOrDefault(u => u.ProductID == id);
                return urn;
            }
        }

        public static List<vw_urunler> kategorisine_gore_urunleri_getir(int id)
        {
            using (iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities())
            {
                int? catID = db.vw_urunler.FirstOrDefault(p => p.ProductID == id).CategoryID;
                List<vw_urunler> liste = db.vw_urunler.Where(p => p.CategoryID == catID && p.ProductID != id).ToList();
                //select * from Products where CategoryID = catID 
                return liste;
            }
        }


        public static List<vw_urunler> markasina_gore_urunleri_getir(int id)
        {
            using (iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities())
            {
                int? marID = db.vw_urunler.FirstOrDefault(p => p.ProductID == id).SupplierID;
            List<vw_urunler> liste = db.vw_urunler.Where(p => p.SupplierID == marID && p.ProductID != id).ToList();
                return liste;
            }
        }


        public static List<vw_urunler> bunabakanlar_urunleri_getir(int id)
        {
            using (iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities())
            {
                int? bunabakanID = db.vw_urunler.FirstOrDefault(p => p.ProductID == id).BunaBakanlar;
                List<vw_urunler> liste = db.vw_urunler.Where(p => p.BunaBakanlar == bunabakanID && p.ProductID != id).ToList();
                return liste;
            }
        }

        public static List<vw_arama> arama_getir(string id)
        {
            List<vw_arama> Arama = new List<vw_arama>();
            using (iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities())
            {
                Arama = db.vw_arama.Where(p => p.ARAMAISMI.Contains(id)).Take(10).ToList();
                return Arama;
            }
        }

    }
}