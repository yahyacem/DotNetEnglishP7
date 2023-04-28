using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot.Net.WebApi.Domain
{
    public class Trade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TradeId { get; private set; }
        public void SetTradeId(int id)
        {
            TradeId = id;
        }
        public string? Account { get; set; }
        public string? Type { get; set; }
        public double? BuyQuantity { get; set; }
        public double? SellQuantity { get; set; }
        public double? BuyPrice { get; set; }
        public double? SellPrice { get; set; }
        public string? Benchmark { get; set; }
        public DateTime? TradeDate { get; set; }
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