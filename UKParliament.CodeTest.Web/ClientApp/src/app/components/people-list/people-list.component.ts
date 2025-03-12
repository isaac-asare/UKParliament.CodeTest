import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { PersonViewModel } from '../../models/person-view-model';
import { PersonService } from '../../services/person.service';

@Component({
  selector: 'app-people-list',
  templateUrl: './people-list.component.html',
  styleUrl: './people-list.component.scss'
})
export class PeopleListComponent implements OnInit {
  people: PersonViewModel[] = [];
  @Output() personSelected = new EventEmitter<PersonViewModel>();

  constructor(private personService: PersonService) { }

  ngOnInit(): void {
    this.loadPeople();
  }

  loadPeople(): void {
    this.personService.getAll().subscribe({
      next: (data) => { 
        this.people = data;
      },
      error: (err) => {
        console.error('Error loading people:', err);
      }
    });
  }

  selectPerson(person: PersonViewModel): void {
    this.personSelected.emit(person);
  }

  refreshList(): void {
    this.loadPeople(); 
  }

}
