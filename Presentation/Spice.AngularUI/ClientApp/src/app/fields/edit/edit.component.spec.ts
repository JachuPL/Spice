import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldEditComponent } from './edit.component';

describe('EditComponent', () => {
  let component: FieldEditComponent;
  let fixture: ComponentFixture<FieldEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FieldEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
