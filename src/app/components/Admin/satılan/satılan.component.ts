import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SepetBilgisi } from 'src/app/models/SepetBilgisi';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-satılan',
  templateUrl: './satılan.component.html',
  styleUrls: ['./satılan.component.scss']
})
export class SatılanComponent implements OnInit {
  satılan: SepetBilgisi[];
  dataSource: any;
  displayedColumns = ['urunfoto', 'satis_Urun_Id', 'satis_Top_Fiyat'];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(
    public servis: ApiService,
    public matdialog: MatDialog
  ) { }
  ngOnInit() {
    this.MarkaListele();
  }
  MarkaListele() {
    this.servis.SepetListe().subscribe((d: any = SepetBilgisi) => {
      this.satılan = d;
      this.dataSource = new MatTableDataSource(this.satılan);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }
}

