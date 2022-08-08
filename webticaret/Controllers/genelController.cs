using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webticaret.Models;
using System.IO;
namespace webticaret.Controllers
{
    public class genelController : Controller
    {
        sqlContext db = new sqlContext();
        public static string yetki = "yetkisiz";
        public static string kayitdurum = "";
        // GET: genel
        public ActionResult cik()
        {
            Session.Abandon();
            yetki = "yetkisiz";
            return RedirectToAction("anasayfa");
        }


        public ActionResult iptal(int id)
        {
            sepet sepetsil = db.sepetler.Find(id);
            db.sepetler.Remove(sepetsil);
            db.SaveChanges();
            return RedirectToAction("sepet");
        }



        public ActionResult adressil(int id)
        {
            var bakkullaniliyormu = (from v in db.siparisler
                                     where v.kullanicidetay_id == id
                                     select v).FirstOrDefault();
            if (bakkullaniliyormu == null)
            {
                kullanicidetay sepetsil = db.kullanicidetaylari.Find(id);
                db.kullanicidetaylari.Remove(sepetsil);
                db.SaveChanges();
            }
            return RedirectToAction("hesabım");
        }


        public ActionResult yetkilisiparis()
        {
            var siparisler = db.siparisler.ToList();
            ViewBag.sip = siparisler;
            return View();
        }


        public ActionResult yetkilisiparisdurumguncelle(int id)
        {

            return View();
        }
        [HttpPost]
        public ActionResult yetkilisiparisdurumguncelle(int id, int durumselect, siparis sip)
        {
            int hedefid = id;
            sip = db.siparisler.Find(hedefid);
            if (durumselect == 1)
            {
                sip.siparisdurumu = "Ürün Hazırlanıyor";
                db.SaveChanges();
                return RedirectToAction("yetkilisiparis");
            }
            else if (durumselect == 2)
            {
                sip.siparisdurumu = "Ürün Hazırlandı";
                db.SaveChanges();
                return RedirectToAction("yetkilisiparis");
            }
            else if (durumselect == 3)
            {
                sip.siparisdurumu = "Ürün Kargoya Verildi";
                db.SaveChanges();
                return RedirectToAction("yetkilisiparis");
            }
            else if (durumselect == 4)
            {
                sip.siparisdurumu = "Ürün Teslim Edildi";
                db.SaveChanges();
                return RedirectToAction("yetkilisiparis");
            }
            else
            {
                ViewBag.mesaj = "Lütfen seçiniz";
            }

            return View();
        }




        public ActionResult hesapguncelleme()
        {
            int idal = Convert.ToInt32(Session["kadiid"]);
            var listeinput = (from v in db.kullanicilar
                              where v.id == idal
                              select v).ToList();
            ViewBag.listekh = listeinput;
            return View();
        }
        [HttpPost]
        public ActionResult hesapguncelleme(kullanicikayit ky, string adi, string sadi, string epost, string takmaadi, string sif)
        {
            int idal = Convert.ToInt32(Session["kadiid"]);

            var sifrecek = (from v in db.kullanicilar
                            where v.id == idal
                            select v.sifre).FirstOrDefault();

            if (sif == sifrecek)
            {
                int hedefid = idal;
                ky = db.kullanicilar.Find(hedefid);
                ky.takmaadi = takmaadi;
                db.SaveChanges();
            }
            else
            {
                ViewBag.mesaj = "Şifreyi lütfen doğru giriniz!";
            }
            var listeinput = (from v in db.kullanicilar
                              where v.id == idal
                              select v).ToList();
            ViewBag.listekh = listeinput;
            return View();
        }

        public ActionResult hesabım()
        {


            return View();
        }
        public ActionResult adresguncelle(int id)
        {
            int idal = Convert.ToInt32(Session["kadiid"]);
            int detayidkullaniciidalma = (from v in db.kullanicidetaylari
                                          where v.id == id
                                          select v.kullaniciid).FirstOrDefault();
            if (idal == detayidkullaniciidalma)
            {
                var listeinput = (from v in db.kullanicidetaylari
                                  where v.id == id
                                  select v).ToList();
                ViewBag.listek = listeinput;
            }
            else
            {
                RedirectToAction("anasayfa");
            }
            return View();
        }
        [HttpPost]
        public ActionResult adresguncelle(int id, kullanicidetay kd, string adres, string il, string ilce, string mahalle, string postakodu, string tel1, string tel2)
        {


            int idal = Convert.ToInt32(Session["kadiid"]);
            int detayidkullaniciidalma = (from v in db.kullanicidetaylari
                                          where v.id == id
                                          select v.kullaniciid).FirstOrDefault();
            if (idal == detayidkullaniciidalma)
            {
                var listeinput = (from v in db.kullanicidetaylari
                                  where v.id == id
                                  select v).ToList();
                ViewBag.listek = listeinput;
            }
            else
            {
                RedirectToAction("anasayfa");
            }

            var bakkullaniliyormu = (from v in db.siparisler
                                     where v.kullanicidetay_id == id
                                     select v).FirstOrDefault();

            if (bakkullaniliyormu == null)
            {
                int hedefid = id;
                kd = db.kullanicidetaylari.Find(hedefid);
                kd.adres = adres;
                kd.sehir = il;
                kd.ilce = ilce;
                kd.mahalle = mahalle;
                kd.postakodu = postakodu;
                kd.telno1 = tel1;
                kd.telno2 = tel2;
                db.SaveChanges();
                return RedirectToAction("hesabım");
            }
            else
            {
                ViewBag.mesaj = "Bu adrese bir sipariş var adres güncellenemez!";
                return View();
            }


        }

        [HttpPost]
        public ActionResult yetkilisiparisiptali(int hiddenid, siparis sip)
        {
            int idal = Convert.ToInt32(Session["kadiid"]);
            int omu = (from v in db.kullanicilar
                       where v.id == idal
                       select v.yetki).FirstOrDefault();
            if (omu == 1)
            {
                int hedefid = hiddenid;
                sip = db.siparisler.Find(hedefid);
                sip.siparisdurumu = "Siparişiniz iptal edildi.";
                db.SaveChanges();
                return RedirectToAction("yetkilisiparis");
            }
            else
            {
                return RedirectToAction("anasayfa");
            }
           
        }

        [HttpPost]
        public ActionResult siparisiptali(int hiddenid, siparis sip)
        {
            if (Session["kadiid"] == null)
            {
                return RedirectToAction("siparisler");
            }
            else
            {
                int idal = Convert.ToInt32(Session["kadiid"]);
                int omu = (from v in db.siparisler
                           where v.id == hiddenid
                           select v.kullanici_id).FirstOrDefault();
                if (omu == idal)
                {
                    bool hadi = (from v in db.siparisler
                                 where v.id == hiddenid
                                 select v.siparisiptalistek).FirstOrDefault();
                    if (hadi == false)
                    {
                        int hedefid = hiddenid;
                        sip = db.siparisler.Find(hedefid);
                        sip.siparisiptalistek = true;
                        sip.siparisdurumu += ", İptal İsteği Alındı.";
                        db.SaveChanges();
                        return RedirectToAction("siparisler");
                    }
                    else
                    {
                        return RedirectToAction("siparisler");
                    }
                }
                else
                {
                    return RedirectToAction("anasayfa");
                }
            }
        }

        public ActionResult adresduzenle()
        {
            int idal = Convert.ToInt32(Session["kadiid"]);
            var kisiseladresler = (from v in db.kullanicidetaylari
                                   where v.kullaniciid == idal
                                   select v).ToList();
            ViewBag.adres = kisiseladresler;
            return View();
        }


        public ActionResult sifredegistir()
        {

            return View();
        }
        [HttpPost]
        public ActionResult sifredegistir(kullanicikayit girenveri, string eksisif, string sif, string siftekrar)
        {
            int idal = Convert.ToInt32(Session["kadiid"]);
            string kullanicieskisifre = (from v in db.kullanicilar
                                         where v.id == idal
                                         select v.sifre).FirstOrDefault();
            if (kullanicieskisifre == eksisif)
            {
                if (sif == siftekrar)
                {
                    int hedefid = idal;
                    girenveri = db.kullanicilar.Find(hedefid);
                    girenveri.sifre = sif;
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.mesaj = "Yeni şifreler uyuşmuyor.";
                }
            }
            else
            {
                ViewBag.mesaj = "Eski şifrenizi yanlış girdiniz.";
            }
            return RedirectToAction("hesabım");
        }


        public ActionResult siparisler(siparis sip)
        {
            int idal = Convert.ToInt32(Session["kadiid"]);
            var siparisliste = (from v in db.siparisler
                                where v.kullanici_id == idal
                                select v).ToList();
            ViewBag.siparisler = siparisliste;
            return View();
        }



        public ActionResult adresekle()
        {


            return View();
        }
        [HttpPost]
        public ActionResult adresekle(kullanicidetay kd, string adres, string il, string ilce, string mahalle, string postakodu, string tel1, string tel2)
        {
            int idal = Convert.ToInt32(Session["kadiid"]);
            kd.kullaniciid = idal;
            kd.adres = adres;
            kd.sehir = il;
            kd.ilce = ilce;
            kd.mahalle = mahalle;
            kd.postakodu = postakodu;
            kd.telno1 = tel1;
            kd.telno2 = tel2;
            db.kullanicidetaylari.Add(kd);
            db.SaveChanges();
            ViewBag.umesaj = "Adres kaydı başarıyla gerçekleşti.";

            return RedirectToAction("satinalma");
        }


        public ActionResult satinalma()
        {
            int idal = Convert.ToInt32(Session["kadiid"]);
            var kisiselurunler = (from v in db.sepetler
                                  where v.kullanici_id == idal
                                  select v).ToList();
            ViewBag.sepet = kisiselurunler;
            double ucret = 0;
            foreach (var item in kisiselurunler)
            {
                var urunfiyatbulmak = (from v in db.urunler
                                       where v.id == item.urun_id
                                       select v).FirstOrDefault();
                ucret += item.adet * urunfiyatbulmak.urunfiyat;
            }

            var kisiseladresler = (from v in db.kullanicidetaylari
                                   where v.kullaniciid == idal
                                   select v).ToList();
            ViewBag.adres = kisiseladresler;

            ViewBag.toplamfiyat = ucret.ToString() + " ₺";
            return View();
        }
        [HttpPost]
        public ActionResult satinalma(siparis sip, int adressecim, string kabulmu)
        {

            if (kabulmu == "evet")
            {
                try
                {
                    int idal = Convert.ToInt32(Session["kadiid"]);
                    var sepeturunal = (from v in db.sepetler
                                       where v.kullanici_id == idal
                                       select v.urun_id).ToList();

                    foreach (var item in sepeturunal)
                    {
                        sip.kullanicidetay_id = adressecim;
                        sip.urun_id = item;
                        sip.siparisdurumu = "Şiparişiniz alındı.";
                        int sepeturunadetal = (from v in db.sepetler
                                               where v.kullanici_id == idal && v.urun_id == item
                                               select v.adet).FirstOrDefault();
                        sip.adet = sepeturunadetal;
                        sip.kullanici_id = idal;

                        int urunsilidbul = (from v in db.sepetler
                                            where v.kullanici_id == idal && v.urun_id == item
                                            select v.id).FirstOrDefault();
                        db.siparisler.Add(sip);
                        db.SaveChanges();

                        sepet sepetsil = db.sepetler.Find(urunsilidbul);
                        db.sepetler.Remove(sepetsil);
                        db.SaveChanges();
                    }

                    return RedirectToAction("siparisler");
                }
                catch (Exception)
                {


                }



            }
            else
            {
                ViewBag.mesaj = "Lütfen sözleşmeyi onaylayınız";
            }
            return View();
        }

        public ActionResult sepet()
        {
            int idal = Convert.ToInt32(Session["kadiid"]);
            var kisiselurunler = (from v in db.sepetler
                                  where v.kullanici_id == idal
                                  select v).ToList();
            ViewBag.sepet = kisiselurunler;
            //toplam fiyat


            double ucret = 0;
            foreach (var item in kisiselurunler)
            {
                var urunfiyatbulmak = (from v in db.urunler
                                       where v.id == item.urun_id
                                       select v).FirstOrDefault();
                ucret += item.adet * urunfiyatbulmak.urunfiyat;
            }

            ViewBag.toplamfiyat = ucret.ToString() + " ₺";
            return View();
        }
        [HttpPost]
        public ActionResult sepet(string adet_txtbox, string sepeturunid_txtbox, sepet sep)
        {
            int hedefid = Convert.ToInt32(sepeturunid_txtbox);
            sep = db.sepetler.Find(hedefid);
            sep.adet = Convert.ToInt32(adet_txtbox);
            db.SaveChanges();
            return RedirectToAction("sepet");
        }



        public ActionResult sepetekle(int id, sepet sep)
        {
            int idalya = Convert.ToInt32(Session["kadiid"]);
            if (Session["kadiid"] != null && yetki == "yetkisiz")
            {
                var urunvarmi = (from v in db.sepetler
                                 where v.kullanici_id == idalya & v.urun_id == id
                                 select v).FirstOrDefault();
                if (urunvarmi == null)
                {
                    sep.kullanici_id = Convert.ToInt32(Session["kadiid"]);
                    sep.urun_id = id;
                    sep.adet = 1;
                    db.sepetler.Add(sep);
                    db.SaveChanges();
                }
            }
            else
            {

            }
            return RedirectToAction("anasayfa");
        }



        public ActionResult urundetay(int id)
        {

            ViewBag.id = id;
            string resim1adi = (from v in db.urunler
                                where v.id == id
                                select v.resim1).FirstOrDefault();
            string resim2adi = (from v in db.urunler
                                where v.id == id
                                select v.resim2).FirstOrDefault();
            string resim3adi = (from v in db.urunler
                                where v.id == id
                                select v.resim3).FirstOrDefault();
            string urunadi = (from v in db.urunler
                              where v.id == id
                              select v.urunadi).FirstOrDefault();
            string aciklamasi = (from v in db.urunler
                                 where v.id == id
                                 select v.urunbilgi).FirstOrDefault();
            int adeti = (from v in db.urunler
                         where v.id == id
                         select v.adet).FirstOrDefault();
            double fiyati = (from v in db.urunler
                             where v.id == id
                             select v.urunfiyat).FirstOrDefault();
            string kar = (from v in db.urunler
                          where v.id == id
                          select v.kargoadi).FirstOrDefault();
            string karbi = (from v in db.urunler
                            where v.id == id
                            select v.kargobilgisi).FirstOrDefault();


            int katidal = (from v in db.urunler
                           where v.id == id
                           select v.kategori_id).FirstOrDefault();
            int altkatidal = (from v in db.urunler
                              where v.id == id
                              select v.altkategori_id).FirstOrDefault();
            string kateadi = (from v in db.kategoriler
                              where v.id == katidal
                              select v.kategoriadi).FirstOrDefault();
            string altkateadi = (from v in db.kategorialtadlari
                                 where v.id == altkatidal
                                 select v.altkategoriadi).FirstOrDefault();
            ViewBag.resim1 = resim1adi;
            ViewBag.resim2 = resim2adi;
            ViewBag.resim3 = resim3adi;
            ViewBag.urunad = urunadi;
            ViewBag.aciklama = aciklamasi;
            ViewBag.adet = adeti;
            ViewBag.fiyat = fiyati;
            ViewBag.kargoadi = kar;
            ViewBag.kargobilgi = karbi;
            ViewBag.kate = kateadi;
            ViewBag.altkate = altkateadi;
            return View();
        }



        public ActionResult altkategoriekle(kategori kat)
        {
            var tumkatlar = db.kategoriler.ToList();
            return View(tumkatlar);
        }
        [HttpPost]
        public ActionResult altkategoriekle(kategorialtadi kataltadi, kategori kat, string kadi, int akatadid)
        {
            kataltadi.anakategori_id = akatadid;
            kataltadi.altkategoriadi = kadi;
            db.kategorialtadlari.Add(kataltadi);
            db.SaveChanges();
            ViewBag.uyari = "Kayıt Başarılı";
            var tumkatlar = db.kategoriler.ToList();
            return View(tumkatlar);
        }










        public ActionResult urunekle()
        {
            //ViewBag.kategorilerr = db.kategoriler.ToList();
            //ViewBag.altkategorilerr = db.kategorialtadlari.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult urunekle(urun urun, string uadi, string ubilgi, double ufiyat, string ukaradi, string ukarbilgi, int uadet, int iller, int ilceler, HttpPostedFileBase resim1, HttpPostedFileBase resim2, HttpPostedFileBase resim3)
        {
            if (ModelState.IsValid && resim1 != null && resim2 != null && resim3 != null)
            {
                string dosyaismi = resim1.FileName;
                string dosyaismi2 = resim2.FileName;
                string dosyaismi3 = resim3.FileName;


                if (dosyaismi.EndsWith("jpg") && dosyaismi2.EndsWith("jpg") && dosyaismi3.EndsWith("jpg"))
                {
                    urun.urunadi = uadi;
                    urun.urunbilgi = ubilgi;
                    urun.urunfiyat = ufiyat;
                    urun.kargoadi = ukaradi;
                    urun.kargobilgisi = ukarbilgi;
                    urun.adet = uadet;
                    urun.kategori_id = iller;
                    urun.altkategori_id = ilceler;
                    urun.resim1 = dosyaismi;
                    urun.resim2 = dosyaismi2;
                    urun.resim3 = dosyaismi3;





                    db.urunler.Add(urun);
                    db.SaveChanges();

                    int sonid = db.urunler.ToList().LastOrDefault().id;


                    string path = Server.MapPath("~/images/" + sonid);
                    Directory.CreateDirectory(path);
                    resim1.SaveAs(Server.MapPath("~/images/" + sonid + "/" + dosyaismi));
                    resim2.SaveAs(Server.MapPath("~/images/" + sonid + "/" + dosyaismi2));
                    resim3.SaveAs(Server.MapPath("~/images/" + sonid + "/" + dosyaismi3));

                    ViewBag.mesaj = "Ürün Kaydı Başarılı";
                }
            }
            return View();
        }


        public ActionResult kategoriekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult kategoriekle(string katadi, kategori katis)
        {
            if (katadi != null && katadi != "")
            {
                katis.kategoriadi = katadi;
                db.kategoriler.Add(katis);
                db.SaveChanges();
                ViewBag.uyari = "Kayıt Başarılı!";
            }
            else
            {
                ViewBag.uyari = "Lütfen bir kategori adı giriniz!";
            }
            return View();
        }


        public ActionResult kayitol()
        {
            if (Session["kadiid"] != null)
            {
                return RedirectToAction("anasayfa");
            }
            else
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult kayitol(kullanicikayit kkaydi, string adi, string sadi, string epost, string takmaadi, string sif, string siftekrar, string kabulmu)
        {

            if (ModelState.IsValid)
            {
                if (kabulmu == "evet")
                {
                    if (siftekrar == sif)
                    {
                        try
                        {
                            kkaydi.ad = adi;
                            kkaydi.soyad = sadi;
                            kkaydi.eposta = epost;
                            kkaydi.takmaadi = takmaadi;
                            kkaydi.sifre = sif;
                            kkaydi.yetki = 0;
                            kkaydi.hesapaktif = false;
                            db.kullanicilar.Add(kkaydi);
                            db.SaveChanges();
                            ViewBag.kayitolmesaj = "Kayıt başarıyla gerçekleşti.";
                            kayitdurum = "kayitevet";
                            return RedirectToAction("anasayfa", "genel");
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        ViewBag.kayitolmesaj = "Şifreler eşleşmiyor!";
                    }
                }
                else
                {
                    ViewBag.kayitolmesaj = "Sözleşme onaylanmadı!";
                }
            }
            else
            {
                ViewBag.kayitolmesaj = "Kullanici kaydı başarısız!";
            }
            return View();
        }



        public ActionResult girisyap()
        {
            if (Session["kadiid"] != null)
            {
                return RedirectToAction("anasayfa");
            }
            else
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult girisyap(string epost, string sifre, kullanicikayit kkayit, string unutma)
        {
            sqlContext db = new sqlContext();
            int kullaniciadi = (from v in db.kullanicilar
                                where v.eposta == epost
                                select v.id).FirstOrDefault();

            var kullaniciparolasibul = (from v in db.kullanicilar
                                        where v.sifre == sifre
                                        select v.id).ToList();
            int kullaniciparolasi = 0;

            foreach (var item in kullaniciparolasibul)
            {
                if (Convert.ToInt32(item) == kullaniciadi)
                {
                    kullaniciparolasi = item;
                }
            }



            string takmaadi1 = (from v in db.kullanicilar
                                where v.id == kullaniciadi
                                select v.takmaadi).FirstOrDefault();


            if (kullaniciadi != 0 && kullaniciparolasi != 0 && kullaniciadi == kullaniciparolasi)
            {

                Session["kadiid"] = kullaniciadi;
                Session["takmaadi"] = takmaadi1;
                int yetki1 = (from v in db.kullanicilar
                              where v.id == kullaniciadi
                              select v.yetki).FirstOrDefault();
                if (yetki1 == 1)
                {
                    yetki = "yetkili";
                }
                else
                {
                    yetki = "yetkisiz";
                }

                if (unutma == "evet")
                {
                    Session.Timeout = 2880;
                }
                else
                {

                }

            }
            else
            {
                ViewBag.girismesaj = "E-Mail veya Şifreniz yanlış!";
            }

            ///////////////////////


            ///////////////////////
            if (Session["kadiid"] != null)
            {
                return RedirectToAction("anasayfa");
            }
            else
            {

            }

            return View();
        }


        public ActionResult anasayfa(int id = 0)
        {

            if (id == 0)
            {
                var katlist = db.kategoriler.ToList();
                ViewBag.kategoriisimleri = katlist;
                var uruns = db.urunler.ToList();
                ViewBag.kayit = uruns;
                var altkatlist = db.kategorialtadlari.ToList();
                ViewBag.altkategoriisimleri = altkatlist;
                if (Session["kadiid"] != null)
                {
                    int idal = Convert.ToInt32(Session["kadiid"]);
                    string adi = (from v in db.kullanicilar
                                  where v.id == idal
                                  select v.ad).FirstOrDefault();
                    string sadi = (from v in db.kullanicilar
                                   where v.id == idal
                                   select v.soyad).FirstOrDefault();
                    ViewBag.adisadi = adi + " " + sadi;

                }
                else
                {

                }
            }
            else
            {
                var katlist = db.kategoriler.ToList();
                ViewBag.kategoriisimleri = katlist;
                var urunkisit = (from v in db.urunler
                                 where v.altkategori_id == id
                                 select v).ToList();
                ViewBag.kayit = urunkisit;
                var altkatlist = db.kategorialtadlari.ToList();
                ViewBag.altkategoriisimleri = altkatlist;
            }

            id = 0;
            return View();

        }
        [HttpPost]
        public ActionResult anasayfa(string ara)
        {
            var katlist = db.kategoriler.ToList();
            ViewBag.kategoriisimleri = katlist;
            var urunkisit = (from veri in db.urunler
                             where veri.urunadi.Contains(ara)
                             select veri).ToList();
            ViewBag.kayit = urunkisit;
            var altkatlist = db.kategorialtadlari.ToList();
            ViewBag.altkategoriisimleri = altkatlist;
            return View();
        }



        //urün ekleye kategoriye göre altkategori listeleme
        [HttpPost]
        public JsonResult IlIlce(int? ilID, string tip)
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            //bu işlem başarılı bir şekilde gerçekleşti mi onun kontrolunnü yapıyorum
            bool basariliMi = true;
            try
            {
                switch (tip)
                {
                    case "ilGetir":
                        //veritabanımızdaki iller tablomuzdan illerimizi sonuc değişkenimze atıyoruz
                        foreach (var il in db.kategoriler.ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = il.kategoriadi,
                                Value = il.id.ToString()
                            });
                        }
                        break;
                    case "ilceGetir":
                        //ilcelerimizi getireceğiz ilimizi selecten seçilen ilID sine göre 
                        foreach (var ilce in db.kategorialtadlari.Where(il => il.anakategori_id == ilID).ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = ilce.altkategoriadi,
                                Value = ilce.id.ToString()
                            });
                        }
                        break;

                    default:
                        break;

                }
            }
            catch (Exception)
            {
                //hata ile karşılaşırsak buraya düşüyor
                basariliMi = false;
                sonuc = new List<SelectListItem>();
                sonuc.Add(new SelectListItem
                {
                    Text = "Bir hata oluştu :(",
                    Value = "Default"
                });

            }
            //Oluşturduğum sonucları json olarak geriye gönderiyorum
            return Json(new { ok = basariliMi, text = sonuc });
        }

    }
}