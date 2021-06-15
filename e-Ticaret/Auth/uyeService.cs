using e_Ticaret.Models;
using e_Ticaret.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Ticaret.Auth
{
    public class uyeService
    {
        Entities4 db = new Entities4();
        public uyeBilgisiModel UyeOturumAc(string kadi, string parola)
        {
            uyeBilgisiModel uye = db.Uye_Bilgi.Where(s => s.uye_E_Mail == kadi && s.uye_Sifre == parola).Select(x => new uyeBilgisiModel()
            {
                uye_Id = x.uye_Id,
                uye_Ad_Soyad = x.uye_Ad_Soyad,
                uye_E_Mail = x.uye_E_Mail,
                uye_Adres_Bilgisi = x.uye_Adres_Bilgisi,
                uye_Sifre = x.uye_Sifre,
                uye_Admin_Bilgisi = x.uye_Admin_Bilgisi,
               

            }).SingleOrDefault();
            return uye;
        }
    }
}