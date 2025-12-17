namespace SurveyApi.Models;

public class Answer
{
    public int Id { get; set; }

    public int SurveyId { get; set; }
    public int QuestionId { get; set; }
    public int Value { get; set; }

    public Survey Survey { get; set; } = null!;
}



