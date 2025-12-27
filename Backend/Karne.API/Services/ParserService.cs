using Karne.API.DTOs;

namespace Karne.API.Services
{
    public class ParserService : IParserService
    {
        public List<ParsedResultDto> ParseFile(string content, ParserConfigDto config)
        {
            var results = new List<ParsedResultDto>();
            using var reader = new StringReader(content);
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var result = new ParsedResultDto();
                try
                {
                    // Basic validation of line length
                    if (line.Length < config.NumberStartIndex + config.NumberLength || 
                        line.Length < config.AnswersStartIndex + config.AnswersLength)
                    {
                        result.IsValid = false;
                        result.ErrorMessage = "Line too short for configured schema.";
                        results.Add(result);
                        continue;
                    }

                    // Extract Student Number
                    result.StudentNumber = line.Substring(config.NumberStartIndex, config.NumberLength).Trim();

                    // Extract Answers
                    result.GivenAnswers = line.Substring(config.AnswersStartIndex, config.AnswersLength).Trim();
                }
                catch (Exception ex)
                {
                    result.IsValid = false;
                    result.ErrorMessage = $"Error parsing line: {ex.Message}";
                }

                results.Add(result);
            }

            return results;
        }
    }
}
