import { Component } from '@angular/core';
import { SurveyFormComponent } from './surveys/survey-form.component';
import { RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [SurveyFormComponent, RouterOutlet],
  template: `
    <h1>Survey System</h1>
     <header class="top-bar">
      <a routerLink="/merchandiser-profile" class="link">
        Merchandiser Info
      </a>
    </header>
    <router-outlet></router-outlet>
    <app-survey-form></app-survey-form>
  `,
  styleUrls: ['./app.css']
})
export class AppComponent {}
