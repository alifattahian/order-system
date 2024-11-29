using Domain.Entities;

namespace Domain.Test
{
    internal static class EntitiesCollection
    {
        public static List<Product> Products { get; set; }
        public static Product Product1 => Products.Where(x => x.Code == "AB-X1").FirstOrDefault();
        public static Product Product2 => Products.Where(x => x.Code == "SC-91").FirstOrDefault();

        public static List<ProductPrice> Prices { get; set; }

        static EntitiesCollection()
        {
            Products = new List<Product>(new[]
            {
                new Product((code:"AB-X1",name: "Flower Pot"),null),
                new Product((code:"SC-91",name: "Lamp"),null)
            });


            Prices = new List<ProductPrice>(new[]
            {
                 new ProductPrice(
                   (product: Product1,
                   validFromDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-30)),
                   validUntilDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                   price: 15000))
                {
                    Profit = 800
                },

                new ProductPrice(
                   (product: Product1,
                   validFromDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                   validUntilDate: DateOnly.FromDateTime(DateTime.Now.AddDays(20)),
                   price: 25000))
                {
                    Profit = 1000,
                    Status=Constants.ProductPriceStatus.Inactive
                },
                 new ProductPrice(
                   (product: Product1,
                   validFromDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                   validUntilDate: DateOnly.FromDateTime(DateTime.Now.AddDays(20)),
                   price: 20000))
                {
                    Profit = 1000
                },
                  new ProductPrice(
                   (product: Product2,
                   validFromDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-10)),
                   validUntilDate: DateOnly.FromDateTime(DateTime.Now.AddDays(20)),
                   price: 5000))
                {
                    Profit = 500
                },


            });

        }
    }
}
