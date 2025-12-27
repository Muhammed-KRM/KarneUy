using Karne.API.DTOs;

namespace Karne.API.Services
{
    public interface IEvaluationService
    {
        Task EvaluateExamAsync(int examId, List<ParsedResultDto> results);
    }
}
