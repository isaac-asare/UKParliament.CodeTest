import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonConsoleComponent } from './person-console.component';

describe('PersonConsoleComponent', () => {
  let component: PersonConsoleComponent;
  let fixture: ComponentFixture<PersonConsoleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PersonConsoleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonConsoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
