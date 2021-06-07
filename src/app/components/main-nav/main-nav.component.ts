import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { ApiService } from 'src/app/services/api.service';
import { KategoriBilgisi } from 'src/app/models/KategoriBilgisi';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.css']
})
export class MainNavComponent {
  kategori!: KategoriBilgisi[];
  uyeadi: string;
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver,
    public servis: ApiService) { }

  ngOnInit() {
    this.KategoriListele();
    if (this.servis.oturumKontrol) {
      this.uyeadi = localStorage.getItem("uyeadi");
    }
  }

  KategoriListele() {
    this.servis.KategoriListe().subscribe((d: any = KategoriBilgisi) => {
      this.kategori = d;
    })
  }
  OturumKapat() {
    localStorage.clear();
    location.href = ("/");
  }
}
