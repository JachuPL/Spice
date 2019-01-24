import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpeciesIndexComponent } from './index.component';

describe('IndexComponent', () => {
  let component: SpeciesIndexComponent;
  let fixture: ComponentFixture<SpeciesIndexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpeciesIndexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpeciesIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
