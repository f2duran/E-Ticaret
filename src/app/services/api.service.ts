
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FavoriBilgisi } from '../models/FavoriBilgisi';
import { KategoriBilgisi } from '../models/KategoriBilgisi';
import { MarkaBilgisi } from '../models/MarkaBilgisi';
import { SepetBilgisi } from '../models/SepetBilgisi';
import { UrunBilgisi } from '../models/UrunBilgisi';
import { UyeBilgisi } from '../models/UyeBilgisi';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  apiUrl = "https://localhost:44337/api/";
  siteurl = "";
  constructor(
    public http: HttpClient
  ) { }

  //üye
  UyeListele() {
    return this.http.get(this.apiUrl + "uyelistele")
  }

  UyeById(uye_Id: string) {
    return this.http.get(this.apiUrl + "uyebyid" + uye_Id);
  }

  UyeEkle(uye: UyeBilgisi) {
    return this.http.post(this.apiUrl + "uyeekle", uye);
  }

  UyeDuzenle(uye: UyeBilgisi) {
    return this.http.put(this.apiUrl + "uyeduzenle", uye);
  }

  UyeSil(Uye_Id: string) {
    return this.http.delete(this.apiUrl + "uyesil/" + Uye_Id);
  }

  UyeSifreYenile(Uye_Id: UyeBilgisi) {
    return this.http.put(this.apiUrl + "sifreyenile", Uye_Id);
  }

  UyeAdresDuzenle(Uye_Id: UyeBilgisi) {
    return this.http.put(this.apiUrl + "adresduzenle", Uye_Id);

  }

  //Ürün
  UrunListele() {
    return this.http.get(this.apiUrl + "urunliste")
  }

  UrunById(urun_Id: string) {
    return this.http.get(this.apiUrl + "urunbyıd" + urun_Id);
  }

  UrunEkle(urun: UrunBilgisi) {
    return this.http.post(this.apiUrl + "urunekle", urun);
  }

  UrunDuzenle(urun: UrunBilgisi) {
    return this.http.put(this.apiUrl + "urunduzenle", urun);
  }

  UrunSil(Urun_Id: string) {
    return this.http.delete(this.apiUrl + "urunsil/" + Urun_Id);
  }

  //Sepet
  SepetListe() {
    return this.http.get(this.apiUrl + "sepetlistele")
  }

  SepetById(Sepet_Id: string) {
    return this.http.get(this.apiUrl + "sepetbyıd" + Sepet_Id);
  }

  SepetEkle(sepet: SepetBilgisi) {
    return this.http.post(this.apiUrl + "sepetekle", sepet);
  }

  SepetSil(Sepet_Id: string) {
    return this.http.delete(this.apiUrl + "sepetsil/" + Sepet_Id);
  }

  //Kategori
  KategoriListe() {
    return this.http.get(this.apiUrl + "kategorilistele")
  }

  KategoriById(Kategori_Id: string) {
    return this.http.get(this.apiUrl + "kategoribyid" + Kategori_Id);
  }

  KategoriEkle(kategori: KategoriBilgisi) {
    return this.http.post(this.apiUrl + "kategoriekle", kategori);
  }

  KategoriSil(Kategori_Id: string) {
    return this.http.delete(this.apiUrl + "kategorisil/" + Kategori_Id);
  }

  KategoriDuzenle(kategori: KategoriBilgisi) {
    return this.http.put(this.apiUrl + "kategoriduzenle", kategori);
  }

  //Marka
  MarkaListe() {
    return this.http.get(this.apiUrl + "markalistele")
  }

  MarkaById(marka_ıd: string) {
    return this.http.get(this.apiUrl + "markabyid" + marka_ıd);
  }

  MarkaEkle(marka: MarkaBilgisi) {
    return this.http.post(this.apiUrl + "markaekle", marka);
  }

  MarkaSil(marka_ıd: string) {
    return this.http.delete(this.apiUrl + "markasil/" + marka_ıd);
  }

  MarkaDuzenle(marka: MarkaBilgisi) {
    return this.http.put(this.apiUrl + "markaduzenle", marka);
  }

  //Favori
  FavoriListe() {
    return this.http.get(this.apiUrl + "favoriliste")
  }

  FavoriByd(favori_Id: string) {
    return this.http.get(this.apiUrl + "favoribyıd" + favori_Id);
  }

  FavoriEkle(favori: FavoriBilgisi) {
    return this.http.post(this.apiUrl + "favoriekle", favori);
  }

  FavoriSil(favori_Id: string) {
    return this.http.delete(this.apiUrl + "favorisil/" + favori_Id);
  }


  //Oturum

  tokenAl(email: string, parola: string) {
    var data = "username=" + email + "&password=" + parola + "&grant_type=password";
    var reqHeader = new HttpHeaders({ "Content-Type": "application/x-www-form-urlencoded" });
    return this.http.post(this.apiUrl + "/token", data, { headers: reqHeader })
  }

  oturumKontrol() {
    if (localStorage.getItem("token")) {
      return true;
    }
    else {
      return false;
    }
  }

  oturumKontrolAdmin() {

    if (localStorage.getItem("uyeYetkileri") == '["Admin"]') {

      return true;
    }
    else {
      return false;
    }
  }
}
