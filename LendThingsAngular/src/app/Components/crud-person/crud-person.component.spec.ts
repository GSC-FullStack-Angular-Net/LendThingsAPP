import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrudPersonComponent } from './crud-person.component';

describe('CrudPersonComponent', () => {
  let component: CrudPersonComponent;
  let fixture: ComponentFixture<CrudPersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CrudPersonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CrudPersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
