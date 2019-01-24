import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldCreateComponent } from './create.component';

describe('CreateComponent', () => {
  let component: FieldCreateComponent;
  let fixture: ComponentFixture<FieldCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FieldCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
