import { Component, Input,Output, EventEmitter, SimpleChanges, OnInit } from '@angular/core';
import { PersonViewModel } from '../../models/person-view-model';
import { PersonService } from '../../services/person.service';
import { Department, DepartmentService } from '../../services/department.service';


@Component({
  selector: 'app-person-editor',
  templateUrl: './person-editor.component.html',
  styleUrl: './person-editor.component.scss'
})

export class PersonEditorComponent implements OnInit {

  @Input() person: PersonViewModel | null = null;

  @Output() personSaved = new EventEmitter<void>();

  departments: Department[] = []; // Store department list
  errors: string[] = [];

  constructor(
    private personService: PersonService,
    private departmentService: DepartmentService
  ) { }

  ngOnInit(): void {
    this.loadDepartments();
  }

  ngOnChanges(changes: SimpleChanges): void {
    // Reset errors when a new person is selected
    if (changes['person']) {
      this.errors = [];
    }
  }

  loadDepartments(): void {
    this.departmentService.getAll().subscribe({
      next: (data) => {
        this.departments = data;
      },
      error: (err) => {
        console.error('Error loading departments:', err);
      }
    });
  }

  save(): void {
    if (!this.person) return;

    if (this.person.id === 0) {
      // Create
      this.personService.create(this.person).subscribe({
        next: (createdPerson) => {
          this.errors = [];
          this.personSaved.emit();
          this.resetForm();
        },
        error: (err) => {
          this.handleErrors(err);
        }
      });
    } else {
      // Update
      this.personService.update(this.person.id, this.person).subscribe({
        next: () => {
          alert('Person updated successfully.');
          this.errors = [];
          this.personSaved.emit(); 
        },
        error: (err) => {
          this.handleErrors(err);
        }
      });
    }
  }

  deletePerson(): void {
    if (!this.person || this.person.id === 0) return;

    const confirmDelete = confirm(`Are you sure you want to delete ${this.person.firstName} ${this.person.lastName}?`);
    if (!confirmDelete) return;

    this.personService.delete(this.person.id).subscribe({
      next: () => {
        alert('Person deleted successfully.');
        this.personSaved.emit();
        this.resetForm(); 
      },
      error: (err) => {
        console.error('Error deleting person:', err);
      }
    });
  }

  private handleErrors(err: any) {
    if (err?.errors) {
      if (Array.isArray(err.errors)) {
        this.errors = err.errors;
      } else {
        this.errors = [JSON.stringify(err.errors)];
      }
    } else {
      this.errors = [JSON.stringify(err)];
    }
  }

  resetForm(): void {
    this.person = {
      id: 0,
      firstName: '',
      lastName: '',
      dateOfBirth: '',
      departmentId: 0,
      departmentName: ''
    };
  }

}
