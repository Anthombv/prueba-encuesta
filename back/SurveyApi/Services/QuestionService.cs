using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.DTOs;

namespace SurveyApi.Services;

public class QuestionService
{
    private readonly SurveyDbContext _context;

    public QuestionService(SurveyDbContext context)
    {
        _context = context;
    }

    public async Task<List<QuestionCategoryDto>> GetQuestionsAsync()
    {
        return await _context.QuestionCategories
            .Include(c => c.Questions)
            .Where(c => c.Questions.Any(q => q.IsActive))
            .Select(c => new QuestionCategoryDto
            {
                CategoryCode = c.Code,
                CategoryName = c.Name,
                Questions = c.Questions
                    .Where(q => q.IsActive)
                    .Select(q => new QuestionDto
                    {
                        Id = q.Id,
                        Text = q.Text,
                        MinValue = q.MinValue,
                        MaxValue = q.MaxValue
                    })
                    .ToList()
            })
            .ToListAsync();
    }
}
