using CqrsMediatrExample0.Commands;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CqrsMediatrExample0.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand>
    {
        private FakeDataStore _fakeDataStore;

        public AddProductHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;

        public async Task Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            await _fakeDataStore.AddProduct(command.product);
            return;
        }
    }
}
