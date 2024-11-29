namespace Domain.Entities
{
    public class Product : IProduct
    {
        protected readonly IProductDataService DataService;
        public Product((string code, string name) product, IProductDataService productDataService)
        {
            Code = product.code;
            Name = product.name;
            DataService = productDataService;
        }
        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }

        public decimal? GetPrice()
        {
            return DataService.GetPrice(this);
        }
    }
}