import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ApiService } from 'src/app/services/api.service';
import { ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MarkaBilgisi } from 'src/app/models/MarkaBilgisi';
import { MarkaDialogComponent } from '../../dialogs/marka-dialog/marka-dialog.component';
import { Sonuc } from 'src/app/models/Sonuc';
import { ConfirmDialogComponent } from '../../dialogs/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-markaeklelistele',
  templateUrl: './markaeklelistele.component.html',
  styleUrls: ['./markaeklelistele.component.scss']
})
export class MarkaeklelisteleComponent implements OnInit {
  markalar: MarkaBilgisi[];
  dataSource: any;
  displayedColumns = ['marka_Id', 'marka_Adi', 'islemler'];
  dialogRef: MatDialogRef<MarkaDialogComponent>;
  dialogRefConfirm: MatDialogRef<ConfirmDialogComponent>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(
    public servis: ApiService,
    public matdialog: MatDialog,
  ) { }
  ngOnInit() {
    this.MarkaListele();
  }
  MarkaListele() {
    this.servis.MarkaListe().subscribe((d: any = MarkaBilgisi) => {
      this.markalar = d;
      this.dataSource = new MatTableDataSource(this.markalar);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }

  Ekle() {
    var yeniKayit: MarkaBilgisi = new MarkaBilgisi();
    this.dialogRef = this.matdialog.open(MarkaDialogComponent, {
      width: '500px',
      data: {
        kayit: yeniKayit,
        islem: 'ekle'
      }
    });
    this.dialogRef.afterClosed().subscribe(d => {
      if (d) {
        this.servis.MarkaEkle(d).subscribe((s: Sonuc) => {
          console.log(s);
          this.MarkaListele();
        });
      }
    });
  }
  Duzenle(kayit: MarkaBilgisi) {
    this.dialogRef = this.matdialog.open(MarkaDialogComponent, {
      width: '500px',
      data: {
        kayit: kayit,
        islem: 'duzenle'
      }
    });
    this.dialogRef.afterClosed().subscribe(d => {
      kayit.marka_Adi = d.marka_Adi;
      if (d) {
        this.servis.MarkaDuzenle(kayit).subscribe((s: Sonuc) => {
          console.log(s);
          this.MarkaListele();
        });
      }
    });
  }

  Sil(kayit: MarkaBilgisi) {
    this.dialogRefConfirm = this.matdialog.open(ConfirmDialogComponent, {
      width: '400px'
    });
    this.dialogRefConfirm.componentInstance.dialogMesaj = kayit.marka_Adi + " Markası Silinecektir Onaylıyor Musunuz?";
    this.dialogRefConfirm.afterClosed().subscribe(d => {
      if (d) {
        this.servis.MarkaSil(kayit.marka_Id).subscribe((s: Sonuc) => {
          console.log(s);
          this.MarkaListele();
        });
      }
    });
  }
}

