/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SepetComponent } from './sepet.component';

describe('SepetComponent', () => {
  let component: SepetComponent;
  let fixture: ComponentFixture<SepetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SepetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SepetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
