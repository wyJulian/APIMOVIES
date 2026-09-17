namespace Domain.Users
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string NormalizedUserName { get; set; }
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
        public required string NormalizedEmail { get; set; }
        public required string FirstName { get; set; }
        public required string NormalizedFirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? NormalizedMiddleName { get; set; }
        public required string LastName { get; set; }
        public required string NormalizedLastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
        public string? NormalizedPhoneNumber { get; set; }
        public bool PhoneConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string? TwoFactorChannel { get; set; }
    }
}
