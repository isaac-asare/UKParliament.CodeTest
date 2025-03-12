import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { PersonViewModel } from '../models/person-view-model';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private personApiUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string)
  {
    this.personApiUrl = `${this.baseUrl}api/person`;
  }


  getAll(): Observable<PersonViewModel[]> {
    return this.http.get<PersonViewModel[]>(this.personApiUrl)
      .pipe(catchError(this.handleError));
  }


  getById(id: number): Observable<PersonViewModel> {
    return this.http.get<PersonViewModel>(`${this.personApiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  create(person: PersonViewModel): Observable<PersonViewModel> {
    return this.http.post<PersonViewModel>(this.personApiUrl, person)
      .pipe(catchError(this.handleError));
  }


  update(id: number, person: PersonViewModel): Observable<void> {
    return this.http.put<void>(`${this.personApiUrl}/${id}`, person)
      .pipe(catchError(this.handleError));
  }


  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.personApiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }


  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${JSON.stringify(error.error)}`);
    }
    // Return a user-facing error
    return throwError(() => error.error || 'Server error');
  }
}
