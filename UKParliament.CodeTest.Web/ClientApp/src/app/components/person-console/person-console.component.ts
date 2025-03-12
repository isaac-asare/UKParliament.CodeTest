import { Component } from '@angular/core';
import { PersonViewModel } from '../../models/person-view-model';


@Component({
  selector: 'app-person-console',
  templateUrl: './person-console.component.html',
  styleUrl: './person-console.component.scss'
})

export class PersonConsoleComponent {

  selectedPerson: PersonViewModel | null = null;

  onPersonSelected(person: PersonViewModel): void {
    this.selectedPerson = { ...person }; 
  }

  addNewPerson(): void {
    this.selectedPerson = {
      id: 0,
      firstName: '',
      lastName: '',
      dateOfBirth: '',
      departmentId: 0,
      departmentName: ''
    };
  }


}
