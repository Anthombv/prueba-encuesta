import { Routes } from '@angular/router';
import { SurveyFormComponent } from './pages/survey-form/survey-form';
import { SurveysList } from './pages/surveys-list/surveys-list';

export const routes: Routes = [
  { path: '', component: SurveyFormComponent },
  { path: 'surveys', component: SurveysList },
  { path: '**', redirectTo: '' }
];