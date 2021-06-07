import { Sonuc } from './../../../models/Sonuc';
import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { MyAlertService } from 'src/app/services/myAlert.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    public apiservis: ApiService,
    public alert: MyAlertService

  ) { }

  ngOnInit() {
  }
  OturumAc(email: string, parola: string) {
    this.apiservis.tokenAl(email, parola).subscribe((d: any) => {
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
    }, err => {
      var s: Sonuc = new Sonuc();
      s.islem = false;
      s.mesaj = "e-mail veya şifre yanlış!"
      this.alert.AlertUygula(s);
    });
  }
}
