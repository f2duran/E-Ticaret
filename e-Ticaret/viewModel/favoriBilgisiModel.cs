using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Ticaret.viewModel
{
    public class favoriBilgisiModel
    {
        public string favori_Id { get; set; }
        public string favori_Urun_ıd { get; set; }
        public string favori_Uye_Id { get; set; }

        public uyeBilgisiModel uyebilgi { get; set; }
        public urunBilgisiModel urunbilgi { get; set; }

    }
}