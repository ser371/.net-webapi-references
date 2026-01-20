using MediatR;
using Microsoft.AspNetCore.Mvc;

//Controller
//   ↓
//IMediator.Send(GetProductsQuery)
//   ↓
//MediatR finds the matching handler
//   ↓
//IRequestHandler<GetProductsQuery, IEnumerable<Product>>
//   ↓
//Handler executes logic
//   ↓
//Result returned to controller


namespace CqrsMediatrExample0.Queries
{
    public record GetProductsQuery : IRequest<IEnumerable<Product>>;
}
