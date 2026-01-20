using CqrsMediatrExample0.Queries;
using MediatR;

namespace CqrsMediatrExample0.Handlers
{
    //IRequestHandler<TRequest, TResponse> handling request
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly FakeDataStore _fakeDataStore;

        // constructor
        public GetProductsHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;

        // async, getAllProduct, 
        //GetProductsQuery request : This object contains any parameters needed to execute the logic.
        // ancellationToken cancellationToken: it allows the system to stop the operation if it’s no longer needed.
        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _fakeDataStore.GetAllProducts();
        }
    }
}
