namespace SurveyApi.DTOs;

public class QuestionCategoryDto
{
    public string CategoryCode { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public List<QuestionDto> Questions { get; set; } = new();
}
