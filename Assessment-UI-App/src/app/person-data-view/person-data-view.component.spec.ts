import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonDataViewComponent } from './person-data-view.component';

describe('PersonDataViewComponent', () => {
  let component: PersonDataViewComponent;
  let fixture: ComponentFixture<PersonDataViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PersonDataViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonDataViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
