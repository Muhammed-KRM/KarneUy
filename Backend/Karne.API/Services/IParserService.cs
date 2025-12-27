using Karne.API.DTOs;

namespace Karne.API.Services
{
    public interface IParserService
    {
        List<ParsedResultDto> ParseFile(string content, ParserConfigDto config);
    }
}
