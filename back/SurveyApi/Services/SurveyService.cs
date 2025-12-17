using SurveyApi.Data;
using SurveyApi.DTOs;
using SurveyApi.Models;

namespace SurveyApi.Services;

public class SurveyService
{
    private readonly SurveyDbContext _context;

    public SurveyService(SurveyDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateSurveyAsync(CreateSurveyDto dto)
    {
        var survey = new Survey
        {
            CreatedAt = DateTime.UtcNow
        };

        _context.Surveys.Add(survey);
        await _context.SaveChangesAsync();

        var answers = dto.Answers.Select(a => new Answer
        {
            SurveyId = survey.Id,
            QuestionId = a.QuestionId,
            Value = a.Value
        });

        _context.Answers.AddRange(answers);
        await _context.SaveChangesAsync();

        return survey.Id;
    }
}
