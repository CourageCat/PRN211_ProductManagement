namespace SWP.ProductManagement.API.ResponseModels
{
    public class ProductResponseModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryName { get; set; }
        //public int CategoryId { get; set; }
    }
}
