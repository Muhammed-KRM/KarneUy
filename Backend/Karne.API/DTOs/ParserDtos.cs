namespace Karne.API.DTOs
{
    public class ParsedResultDto
    {
        public string StudentNumber { get; set; } = string.Empty;
        public string GivenAnswers { get; set; } = string.Empty;
        public bool IsValid { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public class ParserConfigDto
    {
        public int NumberStartIndex { get; set; }
        public int NumberLength { get; set; }
        public int AnswersStartIndex { get; set; }
        public int AnswersLength { get; set; }
    }
}
