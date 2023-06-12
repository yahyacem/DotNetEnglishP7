using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetEnglishP7.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public required string FullName { get; set; }
    }
}