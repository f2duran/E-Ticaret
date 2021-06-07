import { MarkaeklelisteleComponent } from './components/Admin/markaeklelistele/markaeklelistele.component';
import { UrunListeleComponent } from './components/Admin/urunListele/urunListele.component';
import { ApiService } from './services/api.service';

import { ConfirmDialogComponent } from './components/dialogs/confirm-dialog/confirm-dialog.component';
import { MyAlertService } from './services/myAlert.service';
import { AlertDialogComponent } from './components/dialogs/alert-dialog/alert-dialog.component';
import { MaterialModule } from './material.module';
import { HomeComponent } from './components/home/home.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { MainNavComponent } from './components/main-nav/main-nav.component';
import { UrunEkleComponent } from './components/Admin/urunEkle/urunEkle.component';
import { LoginComponent } from './components/login/login/login.component';
import { SepetComponent } from './components/uye/sepet/sepet.component';
import { KategorieklelisteleComponent } from './components/Admin/kategorieklelistele/kategorieklelistele.component';
import { SatılanComponent } from './components/Admin/satılan/satılan.component';
import { MarkaDialogComponent } from './components/dialogs/marka-dialog/marka-dialog.component';
import { KategoriDialogComponent } from './components/dialogs/kategori-dialog/kategori-dialog.component';
import { RegisterComponent } from './components/login/register/register.component';
import { UyeIslemleriComponent } from './components/Admin/uyeIslemleri/uyeIslemleri.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MainNavComponent,
    UrunListeleComponent,
    UrunEkleComponent,
    LoginComponent,
    SepetComponent,
    KategorieklelisteleComponent,
    MarkaeklelisteleComponent,
    SatılanComponent,
    RegisterComponent,
    UyeIslemleriComponent,

    //Dialoglar
    AlertDialogComponent,
    ConfirmDialogComponent,
    MarkaDialogComponent,
    KategoriDialogComponent,



  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  entryComponents: [
    AlertDialogComponent,
    ConfirmDialogComponent,
    MarkaDialogComponent,
    KategoriDialogComponent,
  ],
  providers: [MyAlertService, ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
