using Microsoft.AspNetCore.Identity;

namespace DotNetEnglishP7.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole() : base() { }
        public AppRole(string roleName)
        {
            Name = roleName;
        }
    }
}
