import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldIndexComponent } from './index.component';

describe('FieldIndexComponent', () => {
  let component: FieldIndexComponent;
  let fixture: ComponentFixture<FieldIndexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FieldIndexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
