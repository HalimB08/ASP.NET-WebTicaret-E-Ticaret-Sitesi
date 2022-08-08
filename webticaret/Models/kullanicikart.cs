using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webticaret.Models
{
    public class kullanicikart
    {
        public int id { get; set; }
        public int kullaniciid { get; set; }
        public string kartadisadi { get; set; }
        public int yil { get; set; }
        public int ay { get; set; }
        public int cvv { get; set; }
    }
}