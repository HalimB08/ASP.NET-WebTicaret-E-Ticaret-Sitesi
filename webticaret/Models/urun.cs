using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webticaret.Models
{
    public class urun
    {
        public int id { get; set; }
        public int kategori_id { get; set; }
        public int altkategori_id { get; set; }
        public string urunadi { get; set; }
        public string urunbilgi { get; set; }
        public double urunfiyat { get; set; }
        public string kargoadi { get; set; }
        public string kargobilgisi { get; set; }
        public int adet { get; set; }
        public bool yayinda { get; set; }
        public string resim1 { get; set; }
        public string resim2 { get; set; }
        public string resim3 { get; set; }
    }
}