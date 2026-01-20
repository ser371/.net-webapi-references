using MediatR;

namespace CqrsMediatrExample0.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<Product>;

}
