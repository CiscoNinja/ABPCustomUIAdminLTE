import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppsettingComponent } from './appsetting.component';

describe('AppsettingComponent', () => {
  let component: AppsettingComponent;
  let fixture: ComponentFixture<AppsettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppsettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppsettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
