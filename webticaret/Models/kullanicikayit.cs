using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webticaret.Models
{
    public class kullanicikayit
    {
        public int id { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public string eposta { get; set; }
        public string takmaadi { get; set; }
        public string sifre { get; set; }
        public int yetki { get; set; }
        public bool hesapaktif { get; set; }
    }
}