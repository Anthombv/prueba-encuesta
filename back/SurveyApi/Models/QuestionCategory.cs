namespace SurveyApi.Models;

public class QuestionCategory
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<Question> Questions { get; set; } = new List<Question>();
}
