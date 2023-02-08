using iakademi43_proje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iakademi43_proje.MVVM
{
    public class AnaSayfaModel
    {
        public List<vw_urunler> yeniUrunler { get; set; }
        public List<vw_urunler> sliderUrunler { get; set; }
        public vw_urunler gunun_urunu { get; set; }
        public List<vw_urunler> ozelUrunler { get; set; }
        public List<vw_urunler> indirimliUrunler { get; set; }
        public List<vw_urunler> onecikanUrunler { get; set; }
        public List<vw_urunler> coksatanUrunler { get; set; }
        public List<vw_urunler> firsatUrunler { get; set; }
        public List<vw_urunler> yildizliUrunler { get; set; }
        public List<vw_urunler> Urunler { get; set; }
        public List<vw_urunler> Urunler2 { get; set; }
        public List<vw_urunler> BunaBakanUrunler { get; set; }
    }
}