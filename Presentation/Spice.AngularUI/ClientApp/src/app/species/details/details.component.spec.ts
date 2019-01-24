import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpeciesDetailsComponent } from './details.component';

describe('DetailsComponent', () => {
  let component: SpeciesDetailsComponent;
  let fixture: ComponentFixture<SpeciesDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpeciesDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpeciesDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
