import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlantsIndexComponent } from './index.component';

describe('IndexComponent', () => {
  let component: PlantsIndexComponent;
  let fixture: ComponentFixture<PlantsIndexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlantsIndexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlantsIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
