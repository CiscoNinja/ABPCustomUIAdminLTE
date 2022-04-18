import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppmenuComponent } from './appmenu.component';

describe('AppmenuComponent', () => {
  let component: AppmenuComponent;
  let fixture: ComponentFixture<AppmenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppmenuComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppmenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
