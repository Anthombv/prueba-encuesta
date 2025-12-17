namespace SurveyApi.DTOs;

public class CreateSurveyDto
{
    public List<AnswerDto> Answers { get; set; } = new();
}
