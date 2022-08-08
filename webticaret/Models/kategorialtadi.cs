using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webticaret.Models
{
    public class kategorialtadi
    {
        public int id { get; set; }
        public string altkategoriadi { get; set; }
        public int anakategori_id { get; set; }
    }
}