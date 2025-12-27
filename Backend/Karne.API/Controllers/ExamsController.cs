using Karne.API.DTOs;
using Karne.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Karne.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamsController : ControllerBase
    {
        private readonly IParserService _parserService;
        private readonly IEvaluationService _evaluationService;

        public ExamsController(IParserService parserService, IEvaluationService evaluationService)
        {
            _parserService = parserService;
            _evaluationService = evaluationService;
        }

        [HttpPost("{examId}/upload-results")]
        public async Task<IActionResult> UploadExamResults(int examId, [FromForm] IFormFile file, [FromQuery] int numberStart, [FromQuery] int numberLen, [FromQuery] int ansStart, [FromQuery] int ansLen)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var config = new ParserConfigDto
            {
                NumberStartIndex = numberStart,
                NumberLength = numberLen,
                AnswersStartIndex = ansStart,
                AnswersLength = ansLen
            };

            // Basic validation
            if (config.NumberLength == 0 || config.AnswersLength == 0)
                return BadRequest("Invalid parser configuration.");

            string content;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                content = await reader.ReadToEndAsync();
            }

            var parsedResults = _parserService.ParseFile(content, config);
            
            if (parsedResults.All(r => !r.IsValid))
            {
                 return BadRequest("Failed to parse any valid lines from the file.");
            }

            try
            {
                await _evaluationService.EvaluateExamAsync(examId, parsedResults);
                return Ok(new { Message = "File processed and results saved.", ProcessedCount = parsedResults.Count(r => r.IsValid) });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
 