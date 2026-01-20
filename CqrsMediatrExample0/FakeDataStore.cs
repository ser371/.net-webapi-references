namespace CqrsMediatrExample0
{
    public class FakeDataStore
    {
        // creating private class List 
        private static List<Product> _products;

        // creating constructor
        public FakeDataStore()
        {
            _products = new List<Product>
        {
            new Product { Id = 1, Name = "Test Product 1" },
            new Product { Id = 2, Name = "Test Product 2" },
            new Product { Id = 3, Name = "Test Product 3" }
        };
        }

        public async Task AddProduct(Product product)
        {
            _products.Add(product);
            // because AddProduct is void, await cant be use directly
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await Task.FromResult(_products);
        }

        public async Task<Product> GetProductById(int id) =>
        await Task.FromResult(_products.Single(p => p.Id == id));


        public async Task EventOccured(Product product, string ent)
        {
            _products.Single(p => p.Id == product.Id).Name = $"{product.Name} evt: {ent}";
            await Task.CompletedTask;
        }
    }
}


// source https://code-maze.com/net-core-series/
