using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webticaret.Models
{
    public class siparis
    {
        public int id { get; set; }
        public int kullanici_id { get; set; }
        public int kullanicidetay_id { get; set; }
        public int urun_id { get; set; }
        public int adet { get; set; }
        public string siparisdurumu { get; set; }
        public bool siparisiptalistek { get; set; }
    }
}