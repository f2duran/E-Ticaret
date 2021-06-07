import { Component, OnInit } from '@angular/core';
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
  urunModel: UrunBilgisi;
  kategori: KategoriBilgisi;
  marka: MarkaBilgisi;
  constructor(
    public apiservise: ApiService
  ) { }

  ngOnInit() {
    this.MarkaListele();
    this.KategoriListele();
  }
  MarkaListele() {
    this.apiservise.MarkaListe().subscribe((d: MarkaBilgisi) => {
      this.marka = d;
    })
  }

  KategoriListele() {
    this.apiservise.KategoriListe().subscribe((d: KategoriBilgisi) => {
      this.kategori = d;
    })
  }
  Kaydet() {

  }
}
