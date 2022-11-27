import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EliminateFormComponent } from './eliminate-form.component';

describe('EliminateFormComponent', () => {
  let component: EliminateFormComponent;
  let fixture: ComponentFixture<EliminateFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EliminateFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EliminateFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
