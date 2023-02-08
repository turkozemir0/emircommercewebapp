using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iakademi43_proje.Models
{
    public class cls_Categories
    {

        public static bool kategori_ismi_varmi_kontrolu(string kategoriadi)
        {
            using (iakademi43DB_ProjeEntities db = new iakademi43DB_ProjeEntities())
            {
                bool adi = db.tbl_Categories.Any(u => u.CategoryName.ToLower() == kategoriadi);
                return adi;
            }
        }

    }
}