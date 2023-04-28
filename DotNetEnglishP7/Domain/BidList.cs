using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot.Net.WebApi.Domain
{
    public class BidList
    {
        // TODO: Map columns in data table BIDLIST with corresponding fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BidListId { get; private set; }
        public void SetBidListId(int id)
        {
            BidListId = id;
        }
        [Required]
        public required string Account { get; set; }
        [Required]
        public required string Type { get; set; }
        public double? BidQuantity { get; set; }
        public double? AskQuantity { get; set; }
        public double? Bid { get; set; }
        public double? Ask { get; set; }
        public string? Benchmark { get; set; }
        public DateTime? BidListDate { get; set; }
        public string? Commentary { get; set; }
        public string? Security { get; set; }
        public string? Status { get; set; }
        public string? Trader { get; set; }
        public string? Book { get; set; }
        public string? CreationName { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? RevisionName { get; set; }
        public DateTime? RevisionDate { get; set; }
        public string? DealName { get; set; }
        public string? DealType { get; set; }
        public string? SourceListId { get; set; }
        public string? Side { get; set; }
    }
}