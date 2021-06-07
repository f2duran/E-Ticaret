/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { KategoriDialogComponent } from './kategori-dialog.component';

describe('KategoriDialogComponent', () => {
  let component: KategoriDialogComponent;
  let fixture: ComponentFixture<KategoriDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KategoriDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KategoriDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
