namespace SurveyApi.DTOs;

public class QuestionDto
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}
