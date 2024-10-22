namespace SWP.ProductManagement.API.RequestModels
{
    public class ProductSearchRequestModel
    {
        public string? ProductName { get; set; }
        public int? UnitsInStock { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
