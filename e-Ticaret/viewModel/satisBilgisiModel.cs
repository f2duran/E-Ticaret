using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Ticaret.viewModel
{
    public class satisBilgisiModel
    {
        public string satis_Id { get; set; }
        public string satis_Uye_Id { get; set; }
        public string satis_Urun_Id { get; set; }
        public int satis_Top_Fiyat { get; set; }
        public int satis_Urun_Fiyat { get; set; }
        public int satis_Urun_KDV { get; set; }
        public int satis_Urun_Kargo_Fiyat { get; set; }

    }
}