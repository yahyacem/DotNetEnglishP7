﻿namespace DotNetEnglishP7.Identity
{
    public class RegisterUser
    {
        public int? Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public required string Role { get; set; }
    }
}
