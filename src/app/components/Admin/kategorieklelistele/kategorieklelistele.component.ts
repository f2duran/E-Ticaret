import { KategoriBilgisi } from 'src/app/models/KategoriBilgisi';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ApiService } from 'src/app/services/api.service';
import { ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { KategoriDialogComponent } from '../../dialogs/kategori-dialog/kategori-dialog.component';
import { ConfirmDialogComponent } from '../../dialogs/confirm-dialog/confirm-dialog.component';
import { Sonuc } from 'src/app/models/Sonuc';

@Component({
  selector: 'app-kategorieklelistele',
  templateUrl: './kategorieklelistele.component.html',
  styleUrls: ['./kategorieklelistele.component.scss']
})
export class KategorieklelisteleComponent implements OnInit {
  kategoriler: KategoriBilgisi[];
  dataSource: any;
  displayedColumns = ['kategori_Id', 'kategori_Adi', 'islemler'];
  dialogRef: MatDialogRef<KategoriDialogComponent>;
  dialogRefConfirm: MatDialogRef<ConfirmDialogComponent>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(
    public servis: ApiService,
    public matdialog: MatDialog
  ) { }
  ngOnInit() {
    this.KategoriListele();
  }
  KategoriListele() {
    this.servis.KategoriListe().subscribe((d: any = KategoriBilgisi) => {
      this.kategoriler = d;
      this.dataSource = new MatTableDataSource(this.kategoriler);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
    if (this.servis.oturumKontrolAdmin()) {
      location.href = ("admin/kategorieklelistele");
    } else {
      alert("Bi zeki sensin")
      location.href = ("/");
    };
  };
  Ekle() {
    var yeniKayit: KategoriBilgisi = new KategoriBilgisi();
    this.dialogRef = this.matdialog.open(KategoriDialogComponent, {
      width: '500px',
      data: {
        kayit: yeniKayit,
        islem: 'ekle'
      }
    });
    this.dialogRef.afterClosed().subscribe(d => {
      if (d) {
        this.servis.KategoriEkle(d).subscribe((s: Sonuc) => {
          console.log(s);
          this.KategoriListele();
        });
      }
    });
  }
  Duzenle(kayit: KategoriBilgisi) {
    this.dialogRef = this.matdialog.open(KategoriDialogComponent, {
      width: '500px',
      data: {
        kayit: kayit,
        islem: 'duzenle'
      }
    });
    this.dialogRef.afterClosed().subscribe(d => {
      kayit.kategori_Adi = d.kategori_Adi;
      if (d) {
        this.servis.KategoriDuzenle(kayit).subscribe((s: Sonuc) => {
          console.log(s);
          this.KategoriListele();
        });
      }
    });
  }
  Sil(kayit: KategoriBilgisi) {
    this.dialogRefConfirm = this.matdialog.open(ConfirmDialogComponent, {
      width: '400px'
    });
    this.dialogRefConfirm.componentInstance.dialogMesaj = kayit.kategori_Adi + " Kategorisi Silinecektir Onaylıyor Musunuz?";
    this.dialogRefConfirm.afterClosed().subscribe(d => {
      if (d) {
        this.servis.KategoriSil(kayit.kategori_Id).subscribe((s: Sonuc) => {
          console.log(s);
          this.KategoriListele();
        });
      }
    });
  }
}
