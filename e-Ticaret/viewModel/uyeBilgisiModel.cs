using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Ticaret.viewModel
{
    public class uyeBilgisiModel
    {
        public string uye_Id { get; set; }
        public string uye_Ad_Soyad { get; set; }
        public string uye_E_Mail { get; set; }
        public string uye_Sifre { get; set; }
        public string uye_Adres_Bilgisi { get; set; }
        public Nullable<bool> uye_Admin_Bilgisi { get; set; }
    }
}