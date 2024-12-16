using System.ComponentModel.DataAnnotations;

namespace POS.Models
{
    public partial class Stocks
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; } = "";
        public string ItemCode { get; set; } = "";
        public string Category { get; set; } = "";
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
