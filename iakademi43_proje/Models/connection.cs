using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace iakademi43_proje.Models
{
    public class connection
    {

        public static SqlConnection baglanti
        {
            get
            {
                SqlConnection sqlcon = new SqlConnection("Server=.\\SQLEXPRESS;Database=iakademi43DB_Proje;trusted_connection=True;");
                return sqlcon;
            }
        }

    }
}