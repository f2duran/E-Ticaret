import { Sonuc } from 'src/app/models/Sonuc';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { KategoriBilgisi } from 'src/app/models/KategoriBilgisi';
import { MarkaBilgisi } from 'src/app/models/MarkaBilgisi';
import { UrunBilgisi } from 'src/app/models/UrunBilgisi';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-urunEkle',
  templateUrl: './urunEkle.component.html',
  styleUrls: ['./urunEkle.component.css']
})
export class UrunEkleComponent implements OnInit {
  kategori: KategoriBilgisi;
  marka: MarkaBilgisi;
  frm: FormGroup;
  kategori_Id: number;
  marka_Id: number;
  secilenFoto: any;
  foto: UrunBilgisi = new UrunBilgisi();
  constructor(
    public apiservise: ApiService,
    public frmbuilder: FormBuilder
  ) {
    this.frm = new FormGroup({
      urun_Adi: new FormControl(),
      kategori_Id: new FormControl(),
      marka_Id: new FormControl(),
      urun_Stok: new FormControl(),
      urun_Gelis_Fiyat: new FormControl(),
      urun_Satis_Fiyat: new FormControl(),
      urun_KDV: new FormControl(),
      urun_Aciklama: new FormControl()
    })
  }
  ngOnInit() {
    this.MarkaListele();
    this.KategoriListele();
  }
  MarkaListele() {
    this.apiservise.MarkaListe().subscribe((d: MarkaBilgisi) => {
      this.marka = d;
    });
  }
  KategoriListele() {
    this.apiservise.KategoriListe().subscribe((d: KategoriBilgisi) => {
      this.kategori = d;
    });
  }
  Kaydet(frm) {
    var urun: UrunBilgisi = new UrunBilgisi();
    urun.urun_Adi = frm.urun_Adi
    urun.urun_Kategori_Id = frm.kategori_Id
    urun.urun_Marka_Id = frm.marka_Id
    urun.urun_Stok = frm.urun_Stok
    urun.urun_Gelis_Fiyat = frm.urun_Gelis_Fiyat
    urun.urun_Satis_Fiyat = frm.urun_Satis_Fiyat
    urun.urun_KDV = frm.urun_KDV
    urun.urun_Aciklama = frm.urun_Aciklama
    urun.urunfoto = this.foto.urunfoto
    urun.urun_Admin_Bilgi = localStorage.getItem("uyeId")
    urun.urun_Eklenme_Tarih = new Date()
    if (urun) {
      //console.log(urun.urun_Admin_Bilgi);
      this.apiservise.UrunEkle(urun).subscribe((s: Sonuc) => {
        console.log(s);
      });
    } else {
      console.log("hata");
    };
  };
  //şuan Çalışmıyor
  FotoSec(e) {
    var fotolar = e.target.files;
    var foto = fotolar[0];
    var fr = new FileReader();
    fr.onloadend = () => {
      this.secilenFoto = fr.result;
      this.foto.urunfoto = fr.result.toString();
    };
    fr.readAsDataURL(foto);
  }
}
