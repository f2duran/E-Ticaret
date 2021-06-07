import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { KategoriBilgisi } from 'src/app/models/KategoriBilgisi';

@Component({
  selector: 'app-kategori-dialog',
  templateUrl: './kategori-dialog.component.html',
  styleUrls: ['./kategori-dialog.component.scss']
})
export class KategoriDialogComponent implements OnInit {
  dialogBaslik: string;
  islem: string;
  yeniKayit: KategoriBilgisi;
  frm: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<KategoriDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public frmBuild: FormBuilder
  ) {
    this.islem = data.islem;
    this.yeniKayit = data.kayit;
    if (this.islem == 'ekle') {
      this.dialogBaslik = "Kategori Ekle";
      this.yeniKayit = new KategoriBilgisi();
    }
    if (this.islem == 'duzenle') {
      this.dialogBaslik = "Kategori Duzenle";
    }
    this.frm = this.FormOlustur();
  }

  ngOnInit() {
  }

  FormOlustur() {
    return this.frmBuild.group({
      kategori_Adi: [this.yeniKayit.kategori_Adi]
    });
  }
}

