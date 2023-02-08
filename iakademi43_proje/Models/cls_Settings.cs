using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iakademi43_proje.Models
{
    public class cls_Settings
    {

        public static bool ayarlar_kaydet(tbl_Settings set)
        {
            using (iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities())
            {
                try
                {
                    db.tbl_Settings.Add(set);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


    }
}