using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using iakademi43_proje.Models;
using iakademi43_proje.MVVM;
using PagedList;
using PagedList.Mvc;

namespace iakademi43_proje.Controllers
{
    public class HomeController : BaseController
    {
        //products tablosu status = 1 , günün ürünü
        //products tablosu status = 2 , slider
        //products tablosu Adddate kolonu desc = en yeni ürünler
        //products tablosu status = 3 , özel
        //products tablosu Discount kolonu desc = indirimli ürünler
        //products tablosu OneCikanlar kolonu desc = öne cıkanlar
        //products tablosu CokSatanlar kolonu desc = çok satanlar
        //products tablosu fırsat ürünler status = 4 , fırsat
        //products tablosu yıldızlı ürünler status = 5 , yıldız
        int sayfadakiurunsayisi = 0;
        int anasayfadakiurunsayisi = 0;

        iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities();

        public HomeController()
        {
             sayfadakiurunsayisi = ViewBag.sayfadakiurunsayisi;
            anasayfadakiurunsayisi = ViewBag.anasayfadakiurunsayisi;
        }

        cls_urunler curn = new cls_urunler();
        AnaSayfaModel ans = new AnaSayfaModel();
        cls_siparisler cs = new cls_siparisler();
        cls_Users cu = new cls_Users();

        // GET: Home
        public ActionResult Index()
        {
            cls_urunler.sayfadakiurunsayisi = sayfadakiurunsayisi;
            cls_urunler.anasayfadakiurunsayisi = anasayfadakiurunsayisi;

            ans.yeniUrunler = curn.urunler_getir("yeni","anasayfa",0);
             ans.sliderUrunler   = curn.urunler_getir("slider", "anasayfa", 0);
             ans.gunun_urunu = curn.urun_getir();
             ans.ozelUrunler = curn.urunler_getir("özel", "anasayfa", 0);
             ans.indirimliUrunler = curn.urunler_getir("indirim", "anasayfa", 0);
            ans.onecikanUrunler = curn.urunler_getir("onecikanlar", "anasayfa", 0);
            ans.coksatanUrunler = curn.urunler_getir("coksatanlar", "anasayfa", 0);
            ans.firsatUrunler = curn.urunler_getir("firsat", "anasayfa", 0);
            ans.yildizliUrunler = curn.urunler_getir("yildiz", "anasayfa", 0);

            return View(ans);
        }


        public ActionResult EnYeniler()
        {
            cls_urunler.sayfadakiurunsayisi = sayfadakiurunsayisi;
            cls_urunler.anasayfadakiurunsayisi = anasayfadakiurunsayisi;

            ans.yeniUrunler = curn.urunler_getir("yeni","altsayfa", 0);
            return View(ans);
        }

        public PartialViewResult _partial_EnYeniler(string sonrakisayfa)
        {
            cls_urunler.sayfadakiurunsayisi = sayfadakiurunsayisi;
            cls_urunler.anasayfadakiurunsayisi = anasayfadakiurunsayisi;

            int sayfano = Convert.ToInt32(sonrakisayfa);
            ans.yeniUrunler = curn.urunler_getir("yeni", "altsayfa", sayfano);

            return PartialView(ans);
        }


        public ActionResult OzelUrunler()
        {
            cls_urunler.sayfadakiurunsayisi = sayfadakiurunsayisi;
            cls_urunler.anasayfadakiurunsayisi = anasayfadakiurunsayisi;

            ans.ozelUrunler = curn.urunler_getir("özel", "altsayfa", 0);
            return View(ans);
        }

        public PartialViewResult _partial_OzelUrunler(string sonrakisayfa)
        {
            cls_urunler.sayfadakiurunsayisi = sayfadakiurunsayisi;
            cls_urunler.anasayfadakiurunsayisi = anasayfadakiurunsayisi;

            int sayfano = Convert.ToInt32(sonrakisayfa);
            ans.ozelUrunler = curn.urunler_getir("özel", "altsayfa", sayfano);

            return PartialView(ans);
        }



        public ActionResult IndirimliUrunler()
        {
            cls_urunler.sayfadakiurunsayisi = sayfadakiurunsayisi;
            cls_urunler.anasayfadakiurunsayisi = anasayfadakiurunsayisi;

            ans.indirimliUrunler = curn.urunler_getir("indirim", "altsayfa", 0);
            return View(ans);
        }

        public PartialViewResult _partial_IndirimliUrunler(string sonrakisayfa)
        {
            cls_urunler.sayfadakiurunsayisi = sayfadakiurunsayisi;
            cls_urunler.anasayfadakiurunsayisi = anasayfadakiurunsayisi;

            int sayfano = Convert.ToInt32(sonrakisayfa);
            ans.indirimliUrunler = curn.urunler_getir("indirim", "altsayfa", sayfano);

            return PartialView(ans);
        }


        public ActionResult OneCikanlar()
        {
            cls_urunler.sayfadakiurunsayisi = sayfadakiurunsayisi;
            cls_urunler.anasayfadakiurunsayisi = anasayfadakiurunsayisi;

            ans.onecikanUrunler = curn.urunler_getir("onecikanlar", "altsayfa", 0);
            return View(ans);
        }

        public PartialViewResult _partial_OneCikanlar(string sonrakisayfa)
        {
            cls_urunler.sayfadakiurunsayisi = sayfadakiurunsayisi;
            cls_urunler.anasayfadakiurunsayisi = anasayfadakiurunsayisi;

            int sayfano = Convert.ToInt32(sonrakisayfa);
            ans.onecikanUrunler = curn.urunler_getir("onecikanlar", "altsayfa", sayfano);

            return PartialView(ans);
        }

        

        public ActionResult DetayliArama()
        {
            return View();
        }

        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(tbl_Users usr)
        {
            tbl_Users us =  cu.uye_kontrol(usr);

            if (us == null)
            {
                //email,şifre hatalı, tekrar gel
                Session["Mesaj"] = "Login ve/veya şifreniz hatalı.Tekrar deneyiniz.";
                return View();
            }
            else
            {
                Session["userID"] = us.UserID;
                Session["Email"] = us.Email;
                //email,şifre dogru
                if (us.IsAdmin == true)
                {
                    //yönetim paneli
                    return RedirectToAction("AnaSayfa","Admin");
                }
                else
                {
                    //proje ana sayfa
                    return RedirectToAction("Index", "Home");
                }
            }

        }




        public ActionResult Cikis()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult UyeOl()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UyeOl(tbl_Users usr)
        {
            cls_Users u = new cls_Users();
            u.captcha = Session["randstr"].ToString();
            string txtcaptcha = Request.Form["txtcaptcha"];

            if (txtcaptcha == u.captcha)
            {
                //dogru
                usr.IsAdmin = false;
                usr.Active = true;
                bool sonuc =  cls_Users.uye_ekle(usr);
                if (sonuc == true)
                {
                    Session["Mesaj"] = "Üyelik kaydınız Başarılı.İyi Alışverişler.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["Mesaj"] = "Üyelik kaydınız yapılamadı.HATA";
                    return View();
                }
            }
            else
            {
                //yanlıs
                Session["Mesaj"] = "Güvenlik rakamını yanlış girdiniz.";
                return View();
            }
        }


        public ActionResult captcha()
        {
            Bitmap bmp = new Bitmap(60, 20);
            Graphics grp = Graphics.FromImage(bmp);
            grp.Clear(Color.Blue);
            grp.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Font fnt = new Font("Ariel",8,FontStyle.Bold);

            string randstr = "";
            int[] myIntArray = new int[5];
            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                myIntArray[i] = r.Next(0, 9);
                randstr += myIntArray[i].ToString();
            }

            Session["randstr"] = randstr;
            cls_Users u = new cls_Users();
            u.captcha = randstr;

            grp.DrawString(randstr, fnt, Brushes.White, 3, 3);

            Response.ContentType = "image/GIF";
            bmp.Save(Response.OutputStream, ImageFormat.Gif);
            fnt.Dispose();
            grp.Dispose();
            bmp.Dispose();

            return View();
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }

        public ActionResult İletisim()
        {
            return View();
        }



        //ürünlerde sepete ekle tıklanınca, bu metod cookie de (çerez) sepete ekler
        public ActionResult Sepet(int id)
        {
            cls_siparisler s = new cls_siparisler();
          

            //metod cagıralım,metoda id parametresi gönderelim
            cls_urunler.OneCikanlar_kolonunu_arttırsın(id);



            //10=1&20=1&30=4
            HttpCookie cSetCookie;
          
            s.ProductID = id;
            s.adet = 1;

            //ilk kayıt = sepetim olustur
            //kayıt varsa getir , arkasına ekleme yapacagım
            cSetCookie = Request.Cookies.Get("sepetim");

            if (cSetCookie == null)
            {
                cSetCookie = new HttpCookie("sepetim");
                s.sepet = "";
            }
            else
            {
                //çerez doluysa , tarayıcıdaki verileri alıp , sepet değişkenine atıyorum
                s.sepet = cSetCookie.Value;
            }


            //aynı ürün varmı kontrolü,varsa ekleme,yoksa ekle
            if (s.sepete_ekle(id) == true)
            {
                //cookie tanımladık
                cSetCookie.Values.Add(id.ToString(), "1");
                cSetCookie.Expires = DateTime.Now.AddMinutes(60 * 24 * 30); //30 günlük yarattık
                //tarayıcıya gönderelim
                Response.Cookies.Add(cSetCookie);
                Session["Mesaj"] = "Ürün Sepetinize Eklendi.";
            }
            else
            {
                Session["Mesaj"] = "Bu Ürün Zaten Sepetinize Eklenmiş.";
            }

            string url = Request.UrlReferrer.ToString(); //https://localhost:44322/Home/

            if (url.Length > 24)
            {
                string sil = url.Substring(0, 29);
                url = url.Replace(sil, "");
                return RedirectToAction(url);
            }
            else
            {
                return RedirectToAction("Index");
            }
          
        }

        public ActionResult Detaylar(int id)
        {
            //metod cagıralım,metoda id parametresi gönderelim
            cls_urunler.OneCikanlar_kolonunu_arttırsın(id);

            ans.gunun_urunu =  cls_urunler.urun_bilgisi_getir(id);
            ans.Urunler = cls_urunler.kategorisine_gore_urunleri_getir(id);
            ans.Urunler2 = cls_urunler.markasina_gore_urunleri_getir(id);
            ans.BunaBakanUrunler = cls_urunler.bunabakanlar_urunleri_getir(id);
            return View(ans);
        }


        public ActionResult CokSatanlar(int? page)
        {
            var pagenumber = page ?? 1; //page == null ise page = 1

            var ulist = db.vw_urunler.OrderByDescending(c => c.CokSatanlar).ToList();

            var sayfalidata = ulist.ToPagedList(pagenumber, sayfadakiurunsayisi);

            ViewBag.CokSatanlar = sayfalidata;

            return View();
        }


        public ActionResult kategoriler(int? page , int id)
        {
            var pagenumber = page ?? 1; //page == null ise page = 1

            var ulist = db.vw_urunler.Where(c => c.CategoryID == id).ToList();

            var sayfalidata = ulist.ToPagedList(pagenumber, sayfadakiurunsayisi);

            ViewBag.kategoriler = sayfalidata;

            ViewBag.baslik = db.tbl_Categories.FirstOrDefault(c => c.CategoryID == id).CategoryName;

            return View();
        }

        public ActionResult markalar(int? page, int id)
        {
            var pagenumber = page ?? 1; //page == null ise page = 1

            var ulist = db.vw_urunler.Where(c => c.SupplierID == id).ToList();

            var sayfalidata = ulist.ToPagedList(pagenumber, sayfadakiurunsayisi);

            ViewBag.markalar = sayfalidata;

            ViewBag.baslik = db.tbl_Suppliers.FirstOrDefault(c => c.SupplierID == id).BrandName;

            return View();
        }

        public ActionResult Cart()
        {
            cls_siparisler s = new cls_siparisler();
            //sepetten ürün silerken id gönderecegim(sil butonuna tıklayınca).ürünü sepetten silecek ve Cart sayfasını tekrar yükleyecek
            if (Request.QueryString["scid"] != null)
            {
                //silme butonuyla geldi


                int scid = Convert.ToInt32(Request.QueryString["scid"]); //scid = ProductID
                HttpCookie cSetcookie = Request.Cookies.Get("sepetim");
                s.sepet = cSetcookie.Value; //tarayıcıdaki sepetim icindekileri ,sepet proporty sine gönderdik
                s.sepetten_sil(scid);
                HttpCookie cKuki = new HttpCookie("sepetim",s.sepet);
                Response.Cookies.Add(cKuki);
                cKuki.Expires = DateTime.Now.AddMinutes(60);
                Session["Mesaj"] = "Ürün Sepetinizden Silindi";
                List<cls_siparisler> sepet = s.sepetiyazdir();
                ViewBag.Sepetim = sepet;
                ViewBag.sepet_tablo_detay = sepet;
            }
            else
            {
                //header da sepet sayfama git ten geldi
                HttpCookie Setcookie = Request.Cookies.Get("sepetim");
                List<cls_siparisler> sepet;
                //cookie sepeti boşsa
                if (Setcookie == null)
                {
                    Setcookie = new HttpCookie("sepetim");
                    s.sepet = "";
                    sepet = s.sepetiyazdir();
                    ViewBag.Sepetim = sepet;
                    ViewBag.sepet_tablo_detay = sepet;
                }
                else
                {
                    s.sepet = Setcookie.Value.ToString();
                    sepet = s.sepetiyazdir();
                    ViewBag.Sepetim = sepet;
                    ViewBag.sepet_tablo_detay = sepet;
                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult order()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Giris");
            }
            tbl_Users usr = cu.uye_getir(Convert.ToInt32(Session["userID"]));
            return View(usr);
        }


        [HttpPost]
        public ActionResult order(FormCollection frm)
        {
            string txt_tckimlikno = frm["txt_tckimlikno"];
            string txt_vergino = Request.Form["txt_vergino"];

            string creditCartNo = frm["creditCartNo"];
            string creditCartAy = frm["creditCartAy"];
            string creditCartYil = frm["creditCartYil"];
            string creditCartCVS = frm["creditCartCVS"];

            //payu,iyzico
            /*
             * bu kısım , gercek hayatta ,payu ya üye olduktan sonra kullanılacak kısım

            NameValueCollection data = new NameValueCollection();
          // string backref = "https//www.sedattefci.com/backref";
            string backref = "https://localhost:44322/backref";

            

            data.Add("BACK_REF", backref);
            data.Add("CC_CVV", creditCartCVS);
            data.Add("CC_NUMBER", creditCartNo);
            data.Add("EXP_MONTH", creditCartAy);
            data.Add("EXP_YEAR", creditCartYil);

            var deger = "";
            foreach (var item in data)
            {
                var value = item as string;
                var byteCount = Encoding.UTF8.GetByteCount(data.Get(value));
                deger += byteCount + data.Get(value);
            }

            var signatureKey = "size verilen anahtar kelime buraya yazılacak";
            var hash = HashWithSignature(deger, signatureKey);
            data.Add("ORDER_HASH", hash);

            var x = POSTFormPayu("https://secure.payu.com.tr/order/alu/v3", data);

            if (x.Contains("<STATUS>SUCCESS</STATUS>") && x.Contains("<RETURN_CODE>3DS_ENROLLED</RETURN_CODE>"))
            {
                //basarılı
            }
            else
            {
                //hata
            }
            
            /* order.cshtml sayfası  , user bilgileri , controllerdan beklediği icin, ve bu bilgilere [HttpGet] metodunda buldugum icin,onun icin RedirectToAction yaptım*/

            return RedirectToAction("backref");
        }

       

        public static string HashWithSignature(string hashString,string signature)
        {
            var binaryHast = new HMACMD5(Encoding.UTF8.GetBytes(signature)).ComputeHash(Encoding.UTF8.GetBytes(hashString));

            var hash = BitConverter.ToString(binaryHast).Replace("-", String.Empty).ToLowerInvariant();
            return hash;
        }

        public class StringString
        {
            public string Text1 { get; set; }
            public string Text2 { get; set; }
        }

        public static string POSTFormPayu(string url, NameValueCollection data)
        {
            var result = new List<StringString>();
            var webClient = new WebClient();

            try
            {
                string request = Encoding.UTF8.GetString(webClient.UploadValues(url, data)).Trim();
                return request;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult backref()
        {
            //ödeme yaptı ve basarılı.siparişi cookie den alıp,siparis tablosuna yazdıralım
            siparisi_kaydet();
            return RedirectToAction("Onay");
        }

        public static string orderGroupGUID = "";

        public ActionResult siparisi_kaydet()
        {
            HttpCookie cSetCookie = Request.Cookies.Get("sepetim");
            cls_siparisler s = new cls_siparisler();

            if (cSetCookie != null)
            {
                s.sepet = cSetCookie.Value;
                orderGroupGUID = s.cookie_sepetini_siparis_tablosuna_yaz(Convert.ToInt32(Session["userID"]));
                s.sepet = "";
                Response.Cookies["sepetim"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Onay");
        }

        public ActionResult Onay()
        {
            ViewBag.orderGroupGUID = orderGroupGUID;
            return View();
        }


        public ActionResult Siparislerim()
        {
            if (Session["userID"] != null)
            {
                int userID = Convert.ToInt32(Session["userID"]);
                List<vw_siparislerim> list = db.vw_siparislerim.Where(o => o.UserID == userID).ToList();

                if (list.Count == 0)
                {
                    Session["Mesaj"] = "Henüz hiç siparişiniz yok";
                    return RedirectToAction("Index");
                }

                ViewBag.kisiadi = db.tbl_Users.FirstOrDefault(u => u.UserID == userID).NameSurname;
                return View(list);
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }


        [HttpPost]
        public ActionResult dproducts(FormCollection frm, string[] markalar,string stoktavarmi)
        {
            int catID = Convert.ToInt32(frm["CategoryID"]);
            string[] fiyatlar = frm["amount"].Replace("$", "").Replace(" ", "").Split('-');
            string isaret = "";
            if (stoktavarmi == "1")
            {
                isaret = ">0";
            }
            else
            {
                isaret = ">=0";
            }

            ArrayList mr = new ArrayList();
            if (markalar != null && markalar.Length > 0)
            {
                foreach (var item in markalar)
                {
                    mr.Add(item);
                }
            }

            string markalar2 = "";
            for (int i = 0; i < mr.Count; i++)
            {
                if (i != mr.Count - 1)
                {
                    markalar2 += "SupplierID = " + mr[i] + " or ";
                }
                else
                {
                    markalar2 += "SupplierID = " + mr[i];
                }
            }

            /* 
             select * from tbl_Products where 
            categoryID = 4 and
            (SupplierID = 4 or SupplierID = 5) and
            (Price between 41 and 406) and
            Stok >= 0
            order by ProductName
            */

            string all = "select * from tbl_Products where categoryID = "+ catID + " and ("+markalar2+") and (Price between " + fiyatlar[0] +" and " + fiyatlar[1] +") and Stok "+isaret+" order by ProductName";

            ViewBag.urunlers = cs.urunlerigetirdetaylisorgu(all);

            return View();
        }

        public JsonResult AramaSonucuGetir(string id)
        {
            id = id.ToUpper(new System.Globalization.CultureInfo("tr-TR",false));
            List<vw_arama> ulist = cls_urunler.arama_getir(id);
            return Json(ulist, JsonRequestBehavior.AllowGet);
        }



    }
}