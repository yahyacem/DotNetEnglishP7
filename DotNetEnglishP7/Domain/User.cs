using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot.Net.WebApi.Domain
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public void SetId(int id)
        {
            Id = id;
        }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public required string Role { get; set; }
    }
}