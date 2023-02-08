using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace iakademi43_proje.Models
{
    public class cls_Users
    {

        public string captcha { get; set; }


        public tbl_Users uye_kontrol(tbl_Users usr)
        {
            using (iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities())
            {
                usr.Password = MD5Sifrele(usr.Password);
                tbl_Users us = db.tbl_Users.FirstOrDefault(u => u.Email == usr.Email && u.Password == usr.Password);
                return us;
            }
        }

        public static bool uye_ekle(tbl_Users usr)
        {
            using (iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities())
            {
                try
                {
                    usr.Password = MD5Sifrele(usr.Password);
                    db.tbl_Users.Add(usr);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


        public tbl_Users uye_getir(int UserID)
        {
            using (iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities())
            {
                tbl_Users us = db.tbl_Users.FirstOrDefault(u => u.UserID == UserID);
                return us;
            }
        }


        public static string MD5Sifrele(string metin)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] btr = Encoding.UTF8.GetBytes(metin);
            btr = md5.ComputeHash(btr);

            StringBuilder sb = new StringBuilder();

            foreach (byte item in btr)
            {
                sb.Append(item.ToString("x2").ToLower());
            }

            return sb.ToString();
        }


    }
}