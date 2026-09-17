namespace Domain.Users
{
    public class VerificationCode
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Code { get; set; }
        public required string Purpose { get; set; }
        public required string Channel { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
