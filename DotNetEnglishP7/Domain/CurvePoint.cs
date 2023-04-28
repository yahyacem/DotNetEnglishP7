using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dot.Net.WebApi.Domain
{
    public class CurvePoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public void SetId(int id)
        {
            Id = id;
        }
        [Required]
        public required int? CurvePointId { get; set; }
        public DateTime? AsOfDate { get; set; }
        public double? Term { get; set; }
        public double? Value { get; set; }
        public DateTime CreationDate { get; set; }
    }
}