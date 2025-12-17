import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

export type QuestionDto = { id: number; text: string; minValue: number; maxValue: number; };
export type QuestionCategoryDto = { categoryCode: string; categoryName: string; questions: QuestionDto[]; };

export type CreateSurveyDto = { answers: { questionId: number; value: number }[] };

@Injectable({ providedIn: 'root' })
export class ApiService {
  private base = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getQuestions(): Observable<QuestionCategoryDto[]> {
    return this.http.get<QuestionCategoryDto[]>(`${this.base}/api/questions`);
  }

  createSurvey(dto: CreateSurveyDto): Observable<any> {
    return this.http.post(`${this.base}/api/surveys`, dto);
  }
}
