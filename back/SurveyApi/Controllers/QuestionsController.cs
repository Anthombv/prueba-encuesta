using Microsoft.AspNetCore.Mvc;
using SurveyApi.Services;

namespace SurveyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly QuestionService _service;

    public QuestionsController(QuestionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetQuestionsAsync();
        return Ok(result);
    }
}
