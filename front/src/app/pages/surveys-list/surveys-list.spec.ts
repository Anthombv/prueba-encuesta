import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveysList } from './surveys-list';

describe('SurveysList', () => {
  let component: SurveysList;
  let fixture: ComponentFixture<SurveysList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SurveysList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SurveysList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
