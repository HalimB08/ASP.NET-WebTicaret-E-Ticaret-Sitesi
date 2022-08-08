using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace webticaret.Models
{
    public class sqlContext:DbContext
    {
        public sqlContext() : base("sqlim")
        {

        }
        public DbSet<kullanicikayit> kullanicilar { get; set; }
        public DbSet<kullanicidetay> kullanicidetaylari { get; set; }
        public DbSet<kullanicikart> kullanicikartlari { get; set; }
        public DbSet<kategori> kategoriler { get; set; }
        public DbSet<kategorialtadi> kategorialtadlari { get; set; }
        public DbSet<siparis> siparisler { get; set; }
        public DbSet<urun> urunler { get; set; }
        public DbSet<sepet> sepetler { get; set; }
        public DbSet<dogrulama> dogrumalar { get; set; }
    }
}