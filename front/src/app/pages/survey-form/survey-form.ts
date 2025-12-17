import { CommonModule } from '@angular/common';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { QuestionDto, ApiService, QuestionCategoryDto } from '../../services/api';

type QuestionWithRange = QuestionDto & {
  range: number[];
};

type CategoryWithRange = {
  categoryCode: string;
  categoryName: string;
  questions: QuestionWithRange[];
};

@Component({
  selector: 'app-survey-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './survey-form.html',
  styleUrl: './survey-form.scss',
})
export class SurveyFormComponent implements OnInit {

  loading = true;
  saving = false;

  categories: CategoryWithRange[] = [];
  form!: FormGroup;

  constructor(
    private api: ApiService,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({});
    this.loadQuestions();
  }

  private loadQuestions(): void {
    this.api.getQuestions().subscribe({
      next: (data) => {
        this.categories = this.mapCategories(data);
        this.buildForm();
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }

  private mapCategories(data: QuestionCategoryDto[]): CategoryWithRange[] {
    return data.map(c => ({
      categoryCode: c.categoryCode,
      categoryName: c.categoryName,
      questions: c.questions.map(q => ({
        ...q,
        range: Array.from(
          { length: q.maxValue - q.minValue + 1 },
          (_, i) => q.minValue + i
        )
      }))
    }));
  }

  private buildForm(): void {
    const controls: Record<string, FormControl<number | null>> = {};

    for (const c of this.categories) {
      for (const q of c.questions) {
        controls[this.ctrlName(q.id)] = new FormControl<number | null>(null, [
          Validators.required,
          Validators.min(q.minValue),
          Validators.max(q.maxValue),
        ]);
      }
    }

    this.form = this.fb.group(controls);
  }

  ctrlName(id: number): string {
    return `q_${id}`;
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.saving = true;

    const answers = Object.entries(this.form.value).map(([key, value]) => ({
      questionId: Number(key.replace('q_', '')),
      value: Number(value),
    }));

    this.api.createSurvey({ answers }).subscribe({
      next: () => {
        alert('Encuesta guardada correctamente');
        this.form.reset();
        this.saving = false;
      },
      error: (err) => {
        console.error(err);
        alert('Error al guardar');
        this.saving = false;
      }
    });
  }
}
