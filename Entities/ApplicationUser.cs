using Microsoft.AspNetCore.Identity;

namespace BooksApi.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
