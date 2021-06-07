import { Component, OnInit } from '@angular/core';
import { UrunBilgisi } from 'src/app/models/UrunBilgisi';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  //dialogRef: MatDialogRef<ConfirmDialogComponent> | undefined;

  //dialogRef: MatDialogRef<ConfirmDialogComponent> | undefined;
  urunbilgi!: UrunBilgisi[];
  constructor(
    public servis: ApiService
  ) { }

  ngOnInit() {
    this.UrunListele();
  }


  UrunListele() {
    this.servis.UrunListele().subscribe((d: any = UrunBilgisi) => {
      this.urunbilgi = d;
    })
  }


}
