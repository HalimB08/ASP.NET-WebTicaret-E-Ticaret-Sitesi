using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webticaret.Models
{
    public class kullanicidetay
    {
        public int id { get; set; }
        public int kullaniciid { get; set; }
        public string adres { get; set; }
        public string postakodu { get; set; }
        public string ilce { get; set; }
        public string sehir { get; set; }
        public string mahalle { get; set; }
        public string telno1 { get; set; }
        public string telno2 { get; set; }
    }
}