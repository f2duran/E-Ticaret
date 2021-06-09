using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Ticaret.viewModel
{
    public class urunBilgisiModel
    {
        public string urun_Id { get; set; }
        public string urun_Marka_Id { get; set; }
        public string urun_Kategori_Id { get; set; }
        public string urun_Adi { get; set; }
        public int urun_Gelis_Fiyat { get; set; }
        public int urun_Satis_Fiyat { get; set; }
        public int urun_Stok { get; set; }
        public System.DateTime urun_Eklenme_Tarih { get; set; }
        public int urun_KDV { get; set; }
        public Nullable<int> urun_Satılan { get; set; }
        public byte[] urun_İmage { get; set; }
        public string urun_Admin_Bilgi { get; set; }
        public string urun_Aciklama { get; set; }
        public string urun_Foto { get; set; }
        public string urun_Foto_1 { get; set; }
        public string urun_Foto_2 { get; set; }


        public markaBilgisiModel markabilgi{ get; set; }
        public kategoriBilgisiModel kategoribilgi { get; set; }
        public uyeBilgisiModel adminbilgisi { get; set; }

    }
}