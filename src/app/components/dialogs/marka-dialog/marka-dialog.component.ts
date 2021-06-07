import { MarkaBilgisi } from 'src/app/models/MarkaBilgisi';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-marka-dialog',
  templateUrl: './marka-dialog.component.html',
  styleUrls: ['./marka-dialog.component.scss']
})
export class MarkaDialogComponent implements OnInit {
  dialogBaslik: string;
  islem: string;
  yeniKayit: MarkaBilgisi;
  frm: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<MarkaDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public frmBuild: FormBuilder
  ) {
    this.islem = data.islem;
    this.yeniKayit = data.kayit;
    if (this.islem == 'ekle') {
      this.dialogBaslik = "Marka Ekle";
      this.yeniKayit = new MarkaBilgisi();
    }
    if (this.islem == 'duzenle') {
      this.dialogBaslik = "Marka Duzenle";
    }
    this.frm = this.FormOlustur();
  }

  ngOnInit() {
  }

  FormOlustur() {
    return this.frmBuild.group({
      marka_Adi: [this.yeniKayit.marka_Adi]
    });
  }
}

