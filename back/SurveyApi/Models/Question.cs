namespace SurveyApi.Models;

public class Question
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Text { get; set; } = null!;
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public bool IsActive { get; set; }

    public QuestionCategory Category { get; set; } = null!;
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
