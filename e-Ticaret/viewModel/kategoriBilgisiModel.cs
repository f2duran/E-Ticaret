using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Ticaret.viewModel
{
    public class kategoriBilgisiModel
    {
        public string kategori_Id { get; set; }
        public string kategori_Adi { get; set; }
       
        public urunBilgisiModel urunbilgi { get; set; }
    }
}