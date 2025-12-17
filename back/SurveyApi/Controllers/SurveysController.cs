using Microsoft.AspNetCore.Mvc;
using SurveyApi.DTOs;
using SurveyApi.Services;

namespace SurveyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SurveysController : ControllerBase
{
    private readonly SurveyService _service;

    public SurveysController(SurveyService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSurveyDto dto)
    {
        if (dto.Answers == null || !dto.Answers.Any())
            return BadRequest("Answers are required");

        var surveyId = await _service.CreateSurveyAsync(dto);
        return CreatedAtAction(nameof(Create), new { id = surveyId }, null);
    }
}
