using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot.Net.WebApi.Controllers.Domain
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public void SetId(int id)
        {
            Id = id;
        }
        public string? MoodysRating { get; set; }
        public string? SandPRating { get; set; }
        public string? FitchRating { get; set; }
        public int? OrderNumber { get; set; }
    }
}