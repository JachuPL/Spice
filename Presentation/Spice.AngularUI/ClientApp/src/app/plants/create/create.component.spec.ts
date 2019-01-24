import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlantCreateComponent } from './create.component';

describe('CreateComponent', () => {
  let component: PlantCreateComponent;
  let fixture: ComponentFixture<PlantCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlantCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlantCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
