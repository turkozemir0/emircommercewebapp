using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace iakademi43_proje.Models
{
    public class cls_siparisler
    {
        public int ProductID { get; set; }
        public int adet { get; set; }
        public string sepet { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public int Kdv { get; set; }
        public int Discount { get; set; }
        public string PhotoPath { get; set; }


        iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities();



        //sepete ekle metodu
        //10=2&20=1&30=1    =>    ProductID=Adet&(Sonraki ürün)

        //sepete_ekle
        public bool sepete_ekle(int scid)
        {
            bool varmi = true;
            string[] sepetdizi = sepet.Split('&'); //10=2&20=1&30=1 
            //10=2  --> sepetdizi[0]
            //20=1  --> sepetdizi[1]
            //30=1  --> sepetdizi[2]

            for (int i = 0; i < sepetdizi.Length; i++)
            {
                string[] sepetdizi_ProductID_Adet = sepetdizi[i].Split('=');
                //sepetdizi_ProductID_Adet[0] = 10--- ProductID
                //sepetdizi_ProductID_Adet[1] = 2--- Adet
                if (sepetdizi_ProductID_Adet[0] == scid.ToString())
                {
                    //bu ürün daha önceden sepete eklenmiş
                    varmi = false;
                }
            }

            return varmi;
        }


        //header da , sağ üst kösedeki, sepetim özeti kısmına gelince , dönecek olan bilgiler
        public List<cls_siparisler> sepetiyazdir()
        {
            List<cls_siparisler> list = new List<cls_siparisler>();

            string[] sepetdizi = sepet.Split('&'); //10=2&20=1&30=1 

            for (int i = 0; i < sepetdizi.Length; i++)
            {
                string[] sepetdizi_ProductID_Adet = sepetdizi[i].Split('=');
                int sepetid = int.Parse(sepetdizi_ProductID_Adet[0]);
                if (sepetdizi_ProductID_Adet[0] != "")
                {
                    tbl_Products prd = db.tbl_Products.FirstOrDefault(p => p.ProductID == sepetid);
                    cls_siparisler s = new cls_siparisler();
                    s.ProductName = prd.ProductName;
                    s.ProductID = prd.ProductID;
                    s.Kdv = Convert.ToInt32(prd.Kdv);
                    s.Price =Convert.ToDecimal(prd.Price);
                    s.adet = Convert.ToInt32(sepetdizi_ProductID_Adet[1]);
                    s.PhotoPath = prd.PhotoPath;
                    list.Add(s);
                }
            }
            return list;
        }


        //sepet sayfamda,ürünlerin solundaki sil butonuna tıklayınca,ürünü cookie den silecegim
        public void sepetten_sil(int scid)
        {
            string[] sepetdizi = sepet.Split('&'); //10=2&20=1&30=1 
            string yenisepet = "";
            int count = 1;

            for (int i = 0; i < sepetdizi.Length; i++)
            {
                string[] sepetdizi_ProductID_Adet = sepetdizi[i].Split('=');
                if (sepetdizi_ProductID_Adet[0] != scid.ToString())
                {
                    //silinecek ProductID hariç,diger ürünler icin buraya girer
                    if (count == 1)
                    {
                        yenisepet += sepetdizi_ProductID_Adet[0] + "=" + sepetdizi_ProductID_Adet[1];
                        count++;
                    }
                    else
                    {
                        yenisepet +="&" + sepetdizi_ProductID_Adet[0] + "=" + sepetdizi_ProductID_Adet[1];
                    }
                }
            }
            sepet = yenisepet;
        }



        public string cookie_sepetini_siparis_tablosuna_yaz(int ID)
        {
            List<cls_siparisler> sip = sepetiyazdir();

            string orderGroupGUID = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(".", "");

            foreach (var item in sip)
            {
                tbl_Orders ord = new tbl_Orders();
                ord.orderDate = DateTime.Now;
                ord.orderGroupGUID = orderGroupGUID;
                ord.UserID = ID;
                ord.ProductID = item.ProductID; ;
                ord.quantity = item.adet;
                db.tbl_Orders.Add(ord);
                db.SaveChanges();
            }
            return orderGroupGUID;
        }


        public List<cls_siparisler> urunlerigetirdetaylisorgu(string sorgu)
        {
            List<cls_siparisler> prd = new List<cls_siparisler>();

            SqlConnection sqlcon = connection.baglanti;
            SqlCommand sqlcmd = new SqlCommand(sorgu, sqlcon);
            sqlcon.Open();
            SqlDataReader sdr = sqlcmd.ExecuteReader();
            while (sdr.Read())
            {
                cls_siparisler p = new cls_siparisler();
                p.ProductID = Convert.ToInt32(sdr["ProductID"]);
                p.ProductName = sdr["ProductName"].ToString();
                p.Price = Convert.ToDecimal(sdr["Price"]);
                p.PhotoPath = sdr["PhotoPath"].ToString();
                prd.Add(p);
            }
            sqlcon.Close();
            return prd;
        }

    }
}