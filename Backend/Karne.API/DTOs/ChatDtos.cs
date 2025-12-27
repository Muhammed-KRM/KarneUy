namespace Karne.API.DTOs
{
    public class CreateClassDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class JoinClassDto
    {
        public string JoinCode { get; set; } = string.Empty;
    }

    public class SendMessageDto
    {
        public int ClassroomId { get; set; }
        public string Content { get; set; } = string.Empty;
    }

    public class MessageDto
    {
        public int Id { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; // Decrypted
        public DateTime SentAt { get; set; }
    }
}
