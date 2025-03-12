import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { from } from 'rxjs';
import { PersonConsoleComponent } from './components/person-console/person-console.component';
import { PeopleListComponent } from './components/people-list/people-list.component';
import { PersonEditorComponent } from './components/person-editor/person-editor.component';


@NgModule({ declarations: [
  AppComponent,
  PersonConsoleComponent,
  PeopleListComponent,
  PersonEditorComponent
    ],
    bootstrap: [AppComponent], imports: [BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: PersonConsoleComponent, pathMatch: 'full' }
        ])], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
