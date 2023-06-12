namespace DotNetEnglishP7.Domain
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string FullName { get; set; }
        public required string Role { get; set; }
    }
}
