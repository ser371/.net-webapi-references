using MediatR;

namespace CqrsMediatrExample0.Commands
{
    // we are not returing anything
    public record AddProductCommand(Product product) : IRequest;

}
