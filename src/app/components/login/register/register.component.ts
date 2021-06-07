import { Sonuc } from './../../../models/Sonuc';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UyeBilgisi } from 'src/app/models/UyeBilgisi';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  frmGroup: FormGroup;


  constructor(
    public servis: ApiService,
    public frmbuilder: FormBuilder
  ) {
    this.frmGroup = new FormGroup({
      uye_Ad_Soyad: new FormControl(),
      uye_E_Mail: new FormControl(),
      uye_Sifre: new FormControl()

    });
  }

  ngOnInit() {

  }
  OturumAc(email: string, parola: string) {
    this.servis.tokenAl(email, parola).subscribe((d: any) => {
      localStorage.setItem("token", d.access_token);
      localStorage.setItem("uyeId", d.uyeId);
      localStorage.setItem("uyeadi", d.uyeadi);
      localStorage.setItem("uyeYetkileri", d.uyeYetkileri);

      if (localStorage.getItem("uyeYetkileri") == '["Admin"]') {
        location.href = ("/urunlistele");
      }
      else {
        location.href = ("/");
      }
    });
  }

  KayitOl(frmGroup) {
    var uye: UyeBilgisi = new UyeBilgisi();
    uye.uye_Ad_Soyad = frmGroup.uye_Ad_Soyad
    uye.uye_E_Mail = frmGroup.uye_E_Mail
    uye.uye_Sifre = frmGroup.uye_Sifre
    uye.uye_Admin_Bilgisi = false
    if (uye) {
      this.servis.UyeEkle(uye).subscribe((s: Sonuc) => {
        console.log(s);
        this.OturumAc(uye.uye_E_Mail, uye.uye_Sifre)
      });
    }
  }
}
