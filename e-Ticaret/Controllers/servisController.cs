using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using e_Ticaret.Models;
using System.Web.Http;
using e_Ticaret.viewModel;
using System.IO;
using System.Drawing;

namespace e_Ticaret.Controllers
{
    public class servisController : ApiController
    {
        Entities4 db = new Entities4();
        sonucModel sonuc = new sonucModel();

        #region Uye
        //Üye Ekleme
        [HttpPost]
        [Route("api/uyeekle")]
        public sonucModel UyeEkle(uyeBilgisiModel model)
        {
            if (db.Uye_Bilgi.Count(s => s.uye_E_Mail == model.uye_E_Mail) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıtlı kullanıcı tekrar kayıt edilemez!";
                return sonuc;
            }
            Uye_Bilgi yeniUye = new Uye_Bilgi();
            yeniUye.uye_Id = Guid.NewGuid().ToString();
            yeniUye.uye_Ad_Soyad = model.uye_Ad_Soyad;
            yeniUye.uye_E_Mail = model.uye_E_Mail;
            yeniUye.uye_Sifre = model.uye_Sifre;
            yeniUye.uye_Adres_Bilgisi = model.uye_Adres_Bilgisi;
            yeniUye.uye_Admin_Bilgisi = false;
            db.Uye_Bilgi.Add(yeniUye);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni Üye Eklendi";
            return sonuc;
        }
        //Üye Listeleme
        [HttpGet]
        [Route("api/uyelistele")]
        [Authorize(Roles ="Admin")]
        public List<uyeBilgisiModel> UyeListele()
        {
            List<uyeBilgisiModel> liste = db.Uye_Bilgi.Select(x => new uyeBilgisiModel()
            {
                uye_Id = x.uye_Id,
                uye_Adres_Bilgisi = x.uye_Adres_Bilgisi,
                uye_Ad_Soyad = x.uye_Ad_Soyad,
                uye_E_Mail = x.uye_E_Mail,
                uye_Sifre = x.uye_Sifre,
                uye_Admin_Bilgisi=x.uye_Admin_Bilgisi
            }).ToList();
            return liste;
        }
        //Üye Id İle Listeleme
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public uyeBilgisiModel UyeById(string uyeId)
        {
            uyeBilgisiModel kayit = db.Uye_Bilgi.Where(s => s.uye_Id == uyeId).Select(x=>new uyeBilgisiModel() 
            {
                uye_Id = x.uye_Id,
                uye_Adres_Bilgisi = x.uye_Adres_Bilgisi,
                uye_Ad_Soyad = x.uye_Ad_Soyad,
                uye_E_Mail = x.uye_E_Mail,
                uye_Sifre = x.uye_Sifre,
                uye_Admin_Bilgisi=x.uye_Admin_Bilgisi
            }).SingleOrDefault();
            return kayit;
        }
        //Üye Düzenleme
        [HttpPut]
        [Route("api/uyeduzenle")]
        public sonucModel UyeDuzenle(uyeBilgisiModel model)
        {
            Uye_Bilgi uyebul = db.Uye_Bilgi.Where(s => s.uye_Id == model.uye_Id).SingleOrDefault();
            if (uyebul == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "üye bulunamadığı için güncellenemedi";
                return sonuc;
            }
            uyebul.uye_Ad_Soyad = model.uye_Ad_Soyad;
            uyebul.uye_E_Mail = model.uye_E_Mail;
            uyebul.uye_Sifre = model.uye_Sifre;
            uyebul.uye_Adres_Bilgisi = model.uye_Adres_Bilgisi;
            uyebul.uye_Admin_Bilgisi = model.uye_Admin_Bilgisi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Güncellendi";
            return sonuc;
        }
        //Üye Silme
        [HttpDelete]
        [Route("api/uyesil/{uye_Id}")]
        public sonucModel UyeSil(string uye_Id)
        {

            Uye_Bilgi uyeSil = db.Uye_Bilgi.Where(s => s.uye_Id == uye_Id).SingleOrDefault();
            if (uyeSil == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "üye bulunamadığı için silinemedi";
                return sonuc;
            }
            db.Uye_Bilgi.Remove(uyeSil);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "üye silindi";
            return sonuc;
        }
        #endregion
        #region Urun

        //Ürün Ekleme
        [HttpPost]
        [Route("api/urunekle")]
        public sonucModel UrunEkle(urunBilgisiModel model)
        {
            Urun_Bilgisi yeniurun = new Urun_Bilgisi();
            yeniurun.urun_Id = Guid.NewGuid().ToString();
            yeniurun.urun_Adi = model.urun_Adi;
            yeniurun.urun_Gelis_Fiyat = model.urun_Gelis_Fiyat;
            yeniurun.urun_Satis_Fiyat = model.urun_Satis_Fiyat;
            yeniurun.urun_KDV = model.urun_KDV;
            yeniurun.urun_Satılan = model.urun_Satılan;
            yeniurun.urun_Stok = model.urun_Stok;

            yeniurun.urun_Marka_Id = model.urun_Marka_Id;
            yeniurun.urun_Kategori_Id = model.urun_Kategori_Id;
            yeniurun.urun_Admin_Bilgi = model.urun_Admin_Bilgi;
            yeniurun.urun_Eklenme_Tarih = model.urun_Eklenme_Tarih.AddHours(3);
            yeniurun.urun_Tanitim = model.urun_Tanitim;
            yeniurun.urun_Foto_Bilgisi = model.urun_Foto_Bilgisi;
            yeniurun.urun_Aciklama = model.urun_Aciklama;
            yeniurun.urun_Foto1 = model.urun_Foto1;
            yeniurun.urun_Foto_2 = model.urun_Foto_2;
            yeniurun.urun_foto_3 = model.urun_foto_3;



            db.Urun_Bilgisi.Add(yeniurun);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = yeniurun.urun_Id;
            return sonuc;
        }

        //Ürün Silme
        [HttpDelete]
        [Route("api/urunsil/{urun_Id}")]
        [Authorize(Roles ="Admin")]
        public sonucModel UrunSil(string urun_Id)
        {
           
            Urun_Bilgisi urunsil = db.Urun_Bilgisi.Where(s => s.urun_Id ==urun_Id).SingleOrDefault();
            if (urunsil==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "ürün bulunamadığı için silinemedi";
                return sonuc;
            }
            if (db.Sepet_Bilgisi.Count(s => s.sepet_Urun_Id == urun_Id) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu Ürün Bir Sepete Ekli Önce Sepetten Sil...";
                return sonuc;
            }
            db.Urun_Bilgisi.Remove(urunsil);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "ürün silindi";
            return sonuc;
        }

        //Ürün Düzenleme
        [HttpPut]
        [Route("api/urunduzenle/{urun_Id}")]
        [Authorize(Roles = "Admin")]
        public sonucModel UrunDuzenle(urunBilgisiModel model)
        {
            Urun_Bilgisi urunbul = db.Urun_Bilgisi.Where(s => s.urun_Id == model.urun_Id).SingleOrDefault();
            if (urunbul==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün bulunamadığı için silinemedi";
                return sonuc;
            }
            urunbul.urun_Adi = model.urun_Adi;
            urunbul.urun_Kategori_Id = model.urun_Kategori_Id;
            urunbul.urun_KDV = model.urun_KDV;
            urunbul.urun_Marka_Id = model.urun_Marka_Id;
            urunbul.urun_Satis_Fiyat = model.urun_Satis_Fiyat;
            urunbul.urun_Satılan = model.urun_Satılan;
            urunbul.urun_Stok = model.urun_Stok;
            urunbul.urun_Gelis_Fiyat = model.urun_Gelis_Fiyat;
            urunbul.urun_Eklenme_Tarih = model.urun_Eklenme_Tarih;
            urunbul.urun_Foto1 = model.urun_Foto1;
            urunbul.urun_Foto_2 = model.urun_Foto_2;
            urunbul.urun_foto_3 = model.urun_foto_3;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Güncellendi";
            return sonuc;
        }

        //Ürünü ID ile Listeleme ve Bağlı Tabloları Getirme
        [HttpGet]
        [Route("api/urundetaybyid/{urunıd}")]
        public List<urunBilgisiModel> UrunDetayById(string urunıd)
        {
            List<urunBilgisiModel> liste = db.Urun_Bilgisi.Where(s => s.urun_Id == urunıd).Select(x => new urunBilgisiModel()
            {
                urun_Id = x.urun_Id,
                urun_Adi = x.urun_Adi,
                urun_Kategori_Id = x.urun_Kategori_Id,
                urun_Marka_Id = x.urun_Marka_Id,
                urun_Satılan = x.urun_Satılan,
                urun_Admin_Bilgi = x.urun_Admin_Bilgi,
                urun_Satis_Fiyat = x.urun_Satis_Fiyat,
                urun_Eklenme_Tarih = x.urun_Eklenme_Tarih,
                urun_Aciklama=x.urun_Aciklama,
                urun_KDV = x.urun_KDV,
                urun_Stok = x.urun_Stok,
                urun_Foto1=x.urun_Foto1,
                urun_Foto_2=x.urun_Foto_2,
                urun_foto_3 = x.urun_foto_3
              
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.markabilgi = MarkaById(kayit.urun_Marka_Id);
                kayit.kategoribilgi = KategoriById(kayit.urun_Kategori_Id);


            }

            return liste;
        }

        //Ürün Kategori Id Si İle Listeleme ve Bağlı Tabloları Getirme
        [HttpGet]
        [Route("api/urunkatlistebyıd/{katıd}")]
        public List<urunBilgisiModel> UrunKatListeBy(string katıd)
        {
            List<urunBilgisiModel> kayit = db.Urun_Bilgisi.Where(s => s.urun_Kategori_Id == katıd).Select(x => new urunBilgisiModel()
            {
                urun_Id = x.urun_Id,
                urun_Adi = x.urun_Adi,
                urun_Kategori_Id = x.urun_Kategori_Id,
                urun_Marka_Id = x.urun_Marka_Id,
                urun_Satılan = x.urun_Satılan,
                urun_Admin_Bilgi = x.urun_Admin_Bilgi,
                urun_Satis_Fiyat = x.urun_Satis_Fiyat,
                urun_Eklenme_Tarih = x.urun_Eklenme_Tarih,
                urun_Aciklama = x.urun_Aciklama,
                urun_KDV = x.urun_KDV,
                urun_Stok = x.urun_Stok,
                urun_Foto1 = x.urun_Foto1,
                urun_Foto_2 = x.urun_Foto_2,
                urun_foto_3 = x.urun_foto_3,
                urun_Foto_Bilgisi = x.urun_Foto_Bilgisi
            }).ToList();

            return kayit;
        }

        //Ürün Tamamını Listeleme
        [HttpGet]
        [Route("api/urunliste")]
        public List<urunBilgisiModel> UrunListeleNormal()
        {
            List<urunBilgisiModel> liste = db.Urun_Bilgisi.Select(x => new urunBilgisiModel()
            {
                urun_Id = x.urun_Id,
                urun_Adi = x.urun_Adi,
                urun_Kategori_Id = x.urun_Kategori_Id,
                urun_Marka_Id = x.urun_Marka_Id,
                urun_Satılan = x.urun_Satılan,
                urun_Admin_Bilgi = x.urun_Admin_Bilgi,
                urun_Satis_Fiyat = x.urun_Satis_Fiyat,
                urun_Eklenme_Tarih = x.urun_Eklenme_Tarih,
                urun_Aciklama = x.urun_Aciklama,
                urun_KDV = x.urun_KDV,
                urun_Stok = x.urun_Stok,
                urun_Foto1 = x.urun_Foto1,
                urun_Foto_2 = x.urun_Foto_2,
                urun_foto_3 = x.urun_foto_3,
              urun_Foto_Bilgisi =x.urun_Foto_Bilgisi
            }).ToList();
            return liste;
        }

        //Ürün Id İle Listeleme
        [HttpGet]
        [Route("api/urunbyıd/{urunId}")]
        public urunBilgisiModel UrunById(string urunId)
        {
            urunBilgisiModel urunbul = db.Urun_Bilgisi.Where(s => s.urun_Id == urunId).Select(x => new urunBilgisiModel()
            {
                urun_Id = x.urun_Id,
                urun_Adi = x.urun_Adi,
                urun_Kategori_Id = x.urun_Kategori_Id,
                urun_Marka_Id = x.urun_Marka_Id,
                urun_Satılan = x.urun_Satılan,
                urun_Admin_Bilgi = x.urun_Admin_Bilgi,
                urun_Satis_Fiyat = x.urun_Satis_Fiyat,
                urun_Eklenme_Tarih = x.urun_Eklenme_Tarih,
                urun_Aciklama = x.urun_Aciklama,
                urun_KDV = x.urun_KDV,
                urun_Stok = x.urun_Stok,
                urun_Foto_Bilgisi=x.urun_Foto_Bilgisi,
                urun_Foto1 = x.urun_Foto1,
                urun_Foto_2 = x.urun_Foto_2,
                urun_foto_3 = x.urun_foto_3
            }).SingleOrDefault();
            return urunbul;
        }
        #endregion
        #region foto
        //Ürün Fotoğraf Ekleme
        [HttpPost]
        [Route("api/urunfotoguncelle")]
        [Authorize(Roles = "Admin")]
        public sonucModel UrunFotoGuncelle(urunFotoBilgisi model)
        {
            Urun_Bilgisi foto = db.Urun_Bilgisi.Where(s => s.urun_Id == model.urun_Foto_Urun_Id).SingleOrDefault();


            if (foto == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            if (foto.urun_Foto1 != "urun.jpg")
            {
                string yol = System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/" + foto.urun_Foto1);
                if (File.Exists(yol))
                {
                    File.Delete(yol);
                }
            }

            string data = model.urun_Foto_data;
            string base64 = data.Substring(data.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] imgbytes = Convert.FromBase64String(base64);
            string dosyaAdi = foto.urun_Id + model.urun_Foto_Uzanti.Replace("image/", ".");
            using (var ms = new MemoryStream(imgbytes, 0, imgbytes.Length))
            {
                Image img = Image.FromStream(ms, true);
                img.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/" + dosyaAdi));

            }
            foto.urun_Foto1 = dosyaAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Fotoğraf Güncellendi";


            return sonuc;
        }
        #endregion
        #region sepet
        //Sepet Ekleme
        [HttpPost]
        [Route("api/sepetekle")]
        public sonucModel SepetEkle(sepetBilgisiModel model)
        {
            Sepet_Bilgisi yenisepet = new Sepet_Bilgisi();
            yenisepet.sepet_Id = Guid.NewGuid().ToString();
            yenisepet.sepet_Urun_Fiyat = model.sepet_Urun_Fiyat;
            yenisepet.sepet_Urun_Id = model.sepet_Urun_Id;
            yenisepet.sepet_Uye_Id = model.sepet_Uye_Id;

            db.Sepet_Bilgisi.Add(yenisepet);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "sepet Başarıyla Eklendi";
            return sonuc;
        }
        //Sepet Tamamını Listeleme
        [HttpGet]
        [Route("api/sepetlistele")]
        public List<sepetBilgisiModel> SepetListe()
        {
            List<sepetBilgisiModel> liste = db.Sepet_Bilgisi.Select(x => new sepetBilgisiModel()
            {
                sepet_Id = x.sepet_Id,
                sepet_Urun_Id = x.sepet_Urun_Id,
                sepet_Uye_Id = x.sepet_Uye_Id,
                sepet_Urun_Fiyat = x.sepet_Urun_Fiyat,
              
            }).ToList();
            return liste;
        }
        //Sepet Sepet Id si İle Bağlı Tabloları Listeleme Listeleme
        [HttpGet]
        [Route("api/sepetliste/{sepetid}")]
        public List<sepetBilgisiModel> SepetListe(string sepetid)
        {
            List<sepetBilgisiModel> liste = db.Sepet_Bilgisi.Where(s => s.sepet_Id == sepetid).Select(x => new sepetBilgisiModel()
            {
                sepet_Id = x.sepet_Id,
                sepet_Urun_Fiyat = x.sepet_Urun_Fiyat,
                sepet_Urun_Id = x.sepet_Urun_Id,
                sepet_Uye_Id = x.sepet_Uye_Id

            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.uyebilgi = UyeById(kayit.sepet_Uye_Id);
                kayit.urunbilgi = UrunById(kayit.sepet_Urun_Id);


            }
            return liste;
        }
        //Sepet Silme
        [HttpDelete]
        [Route("api/sepetsil/{sepetıd}")]
        public sonucModel SepetSil(string sepetıd)
        {

            Sepet_Bilgisi sepetsil = db.Sepet_Bilgisi.Where(s => s.sepet_Id == sepetıd).SingleOrDefault();
            if (sepetsil == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "sepet bulunamadığı için silinemedi";
                return sonuc;
            }
            db.Sepet_Bilgisi.Remove(sepetsil);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "sepet silindi";
            return sonuc;
        }
        //Sepeti Ürün Id İle silme
        [HttpDelete]
        [Route("api/sepetsilurunıd/{urunıd}")]
        public sonucModel SepetSilUrunId(string urunıd)
        {

            Sepet_Bilgisi sepetsil = db.Sepet_Bilgisi.Where(s => s.sepet_Urun_Id == urunıd).SingleOrDefault();
            if (sepetsil == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "sepet bulunamadığı için silinemedi";
                return sonuc;
            }
            db.Sepet_Bilgisi.Remove(sepetsil);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "sepet silindi";
            return sonuc;
        }
        //Sepet Üyeye Göre Listeleme
        [HttpGet]
        [Route("api/sepetuyebyid/{uyeid}")]
        public List<sepetBilgisiModel> SepetUyeById(string uyeid)
        {
            List<sepetBilgisiModel> liste = db.Sepet_Bilgisi.Where(s => s.sepet_Uye_Id == uyeid).Select(x => new sepetBilgisiModel()
            {
                sepet_Id = x.sepet_Id,
                sepet_Urun_Fiyat = x.sepet_Urun_Fiyat,
                sepet_Urun_Id = x.sepet_Urun_Id,
                sepet_Uye_Id = x.sepet_Uye_Id

            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.uyebilgi = UyeById(kayit.sepet_Uye_Id);
                kayit.urunbilgi = UrunById(kayit.sepet_Urun_Id);


            }
            return liste;
        }

            #endregion
        #region kategori ve marka
        //Kategori Ekle
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/kategoriekle")]
        public sonucModel KategoriEkle(kategoriBilgisiModel model)
        {
            if (db.Kategori_Bilgisi.Count(s => s.kategori_Id == model.kategori_Id) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıtlı kategori tekrar kayıt edilemez!";
                return sonuc;
            }
            Kategori_Bilgisi yenikategori = new Kategori_Bilgisi();
            yenikategori.kategori_Id = Guid.NewGuid().ToString();
            yenikategori.kategori_Adi = model.kategori_Adi;

            db.Kategori_Bilgisi.Add(yenikategori);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni kategori Eklendi";
            return sonuc;
        }
        //Kategori Düzenle
        [HttpPut]
        [Route("api/kategoriduzenle")]
        [Authorize(Roles = "Admin")]
        public sonucModel KategoriDuzenle(kategoriBilgisiModel model)
        {
            Kategori_Bilgisi düzkat = db.Kategori_Bilgisi.Where(s => s.kategori_Id == model.kategori_Id).SingleOrDefault();
            if (düzkat == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı";
                return sonuc;
            }
            düzkat.kategori_Adi = model.kategori_Adi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Güncellendi!";
            return sonuc;
        }
        //Kategori Listele
        
        [HttpGet]
        [Route("api/kategorilistele")]
        public List<kategoriBilgisiModel> KategoriListele()
        {
            List<kategoriBilgisiModel> liste = db.Kategori_Bilgisi.Select(x => new kategoriBilgisiModel()
            {
                kategori_Id = x.kategori_Id,
                kategori_Adi = x.kategori_Adi
            }).ToList();
            return liste;
        }
        //Kategori Id İle Listele
        [HttpGet]
        [Route("api/kategoribyid/{katid}")]
        public kategoriBilgisiModel KategoriById(string katid)
        {
            kategoriBilgisiModel kayit = db.Kategori_Bilgisi.Where(s => s.kategori_Id == katid).Select(x => new kategoriBilgisiModel()
            {
                kategori_Id = x.kategori_Id,
                kategori_Adi = x.kategori_Adi
              
            }).SingleOrDefault();
            return kayit;
        }
       //Kategori Silme
        [HttpDelete]
        [Route("api/kategorisil/{katid}")]
        [Authorize(Roles = "Admin")]
        public sonucModel KategoriSil(string katid)
        {

            Kategori_Bilgisi katsil = db.Kategori_Bilgisi.Where(s => s.kategori_Id == katid).SingleOrDefault();
            if (katsil == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "kategori bulunamadığı için silinemedi";
                return sonuc;
            }
            if (db.Urun_Bilgisi.Count(s => s.urun_Kategori_Id == katid) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu kategorinin içinde bulunduğu ürünler var! Öncelikle bu kategorinin ürünlerini silmeniz gerekmektedir...";
                return sonuc;
            }
            db.Kategori_Bilgisi.Remove(katsil);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "kategori silindi";
            return sonuc;
        }


        //Marka Ekle
        [HttpPost]
        [Route("api/markaekle")]
        [Authorize(Roles = "Admin")]
        public sonucModel MarkaEkle(markaBilgisiModel model)
        {
            if (db.Marka_Bilgi.Count(s => s.marka_Id == model.marka_Id) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıtlı marka tekrar kayıt edilemez!";
                return sonuc;
            }
            Marka_Bilgi markabilgisi = new Marka_Bilgi();
            markabilgisi.marka_Id = Guid.NewGuid().ToString();
            markabilgisi.marka_Adi = model.marka_Adi;
            //marka kategoriıd eklenmedi!!!!!
            db.Marka_Bilgi.Add(markabilgisi);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni marka Eklendi";
            return sonuc;
        }
        //Marka Listele
        [HttpGet]
        [Route("api/markalistele")]
        public List<markaBilgisiModel> MarkaListele()
        {
            List<markaBilgisiModel> liste = db.Marka_Bilgi.Select(x => new markaBilgisiModel()
            {
                marka_Id = x.marka_Id,
                marka_Adi = x.marka_Adi
            }).ToList();
            return liste;
        }
        //Marka Id İle Listeleme
        [HttpGet]
        [Route("api/markabyid/{markaid}")]
        public markaBilgisiModel MarkaById(string markaid)
        {
            markaBilgisiModel kayit = db.Marka_Bilgi.Where(s => s.marka_Id == markaid).Select(x => new markaBilgisiModel()
            {
                marka_Id = x.marka_Id,
                marka_Adi = x.marka_Adi

            }).SingleOrDefault();
            return kayit;
        }
        //Marka Düzenleme
        [HttpPut]
        [Route("api/markaduzenle")]
        [Authorize(Roles = "Admin")]
        public sonucModel MakraDuzenle(markaBilgisiModel model)
        {
            Marka_Bilgi yenimarka = db.Marka_Bilgi.Where(s => s.marka_Id == model.marka_Id).SingleOrDefault();
            if (yenimarka==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Marka Bulunamadı!";
                return sonuc;
            }
            yenimarka.marka_Id = model.marka_Id;
            yenimarka.marka_Adi = model.marka_Adi;
            db.SaveChanges();
            sonuc.islem=true;
            sonuc.mesaj = "Marka Düzenlendi!";
            return sonuc;
        }
        //Marka Silme
        [HttpDelete]
        [Route("api/markasil/{markaid}")]
        [Authorize(Roles = "Admin")]
        public sonucModel MarkaSil(string markaid)
        {
            Marka_Bilgi markasil = db.Marka_Bilgi.Where(s => s.marka_Id == markaid).SingleOrDefault();
            if (markasil == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "marka bulunamadığı için silinemedi";
                return sonuc;
            }
            if (db.Urun_Bilgisi.Count(s=>s.urun_Marka_Id== markaid)>0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu markanın içinde bulunduğu ürünler var! Öncelikle bu markanın ürünlerini silmeniz gerekmektedir...";
                return sonuc;
            }
            db.Marka_Bilgi.Remove(markasil);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "marka silindi";
            return sonuc;
        }
        #endregion
        #region favori
        //Favori Ekleme
        [HttpPost]
        [Route("api/favoriekle")]
        public sonucModel FavoriEkle(favoriBilgisiModel model)
        {

            Favori_Bilgisi yenifavori = new Favori_Bilgisi();
            yenifavori.favori_Id = Guid.NewGuid().ToString();
            yenifavori.favori_Urun_ıd = model.favori_Urun_ıd;
            yenifavori.favori_Uye_Id = model.favori_Uye_Id;
            db.Favori_Bilgisi.Add(yenifavori);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yeni favori Eklendi";
            return sonuc;
        }
        //Favori Listeleme
        [HttpGet]
        [Route("api/favoriliste")]
        public List<favoriBilgisiModel> FavoriListeNormal()
        {
            List<favoriBilgisiModel> liste = db.Favori_Bilgisi.Select(x => new favoriBilgisiModel()
            {
                favori_Id = x.favori_Id,
                favori_Urun_ıd = x.favori_Urun_ıd,
                favori_Uye_Id = x.favori_Uye_Id,

            }).ToList();
            return liste;
        }
        //Favori Listeleme Id ile ve Bağlı Tabloları Getirme
        [HttpGet]
        [Route("api/favoriliste/{favid}")]
        public List<favoriBilgisiModel> FavoriListe(string favid)
        {
            List<favoriBilgisiModel> liste = db.Favori_Bilgisi.Where(s => s.favori_Id == favid).Select(x => new favoriBilgisiModel()
            {
                favori_Id = x.favori_Id,
                favori_Uye_Id = x.favori_Uye_Id,
                favori_Urun_ıd = x.favori_Urun_ıd,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.uyebilgi = UyeById(kayit.favori_Uye_Id);
                kayit.urunbilgi = UrunById(kayit.favori_Urun_ıd);
            }

            return liste;
        }
        //Favori Listeleme Id İle
        [HttpGet]
        [Route("api/favoribyıd/{fav_ıd}")]
        public favoriBilgisiModel FavoriById(string fav_ıd)
        {
            favoriBilgisiModel fav = db.Favori_Bilgisi.Where(s => s.favori_Id == fav_ıd).Select(x => new favoriBilgisiModel()
            {
                favori_Id = x.favori_Id,
                favori_Urun_ıd = x.favori_Urun_ıd,
                favori_Uye_Id = x.favori_Uye_Id
            }).SingleOrDefault();
            return fav;
        }
        //Favori Silme
        [HttpDelete]
        [Route("api/favorisil/{fav_ıd}")]
        public sonucModel FavoriSil(string fav_ıd)
        {
            Favori_Bilgisi favbul = db.Favori_Bilgisi.Where(s => s.favori_Id == fav_ıd).SingleOrDefault();
            if (favbul==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Favori Bilgisi Bulunamadı!";
                return sonuc;
            }
            db.Favori_Bilgisi.Remove(favbul);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Favori silindi";
            return sonuc;
        }
        #endregion
    }
}
