namespace POS.Dto
{
    public partial class ProductDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = "";
        public string Category { get; set; } = "";
        public float Price { get; set; }
    }
}
