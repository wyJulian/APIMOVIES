namespace APIMOVIES.Settings
{
    public class EmailSetting
    {
        public required string Host { get; set; }
        public required int Port { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FromAddress { get; set; }
        public required string FromName { get; set; }
        public required string ConfirmationUrlBase { get; set; }
    }
}
