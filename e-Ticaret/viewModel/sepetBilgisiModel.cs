using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Ticaret.viewModel
{
    public class sepetBilgisiModel
    {
        public string sepet_Id { get; set; }
        public string sepet_Uye_Id { get; set; }
        public string sepet_Urun_Id { get; set; }
        public int sepet_Urun_Fiyat { get; set; }

        public uyeBilgisiModel uyebilgi { get; set; }
        public urunBilgisiModel urunbilgi { get; set; }
    }
}