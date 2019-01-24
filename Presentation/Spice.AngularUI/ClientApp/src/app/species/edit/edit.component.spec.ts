import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpeciesEditComponent } from './edit.component';

describe('EditComponent', () => {
  let component: SpeciesEditComponent;
  let fixture: ComponentFixture<SpeciesEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpeciesEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpeciesEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
