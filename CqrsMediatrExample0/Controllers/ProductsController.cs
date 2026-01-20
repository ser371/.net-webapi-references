using CqrsMediatrExample0.Commands;
using CqrsMediatrExample0.Notification;
using CqrsMediatrExample0.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;

namespace CqrsMediatrExample0.Controllers
{
    [ApiController]
    [Route("api/v0/text")]
    public class ProductsController : ControllerBase
    {
       private FakeDataStore _fakeDataStore;

        // cretating instance field
        public readonly IMediator _mediator;
        // adding constructor to initalise mediator
        // IMediator interface allow us to send message to MediatR, which then dispatch to relevent handler.It uses DI.
        public ProductsController(IMediator mediator, FakeDataStore fakeDataStore)
        {
            _mediator = mediator;
            _fakeDataStore = fakeDataStore;
        }


        [HttpGet("hello")]
        public IActionResult Get()
        {
            return Ok("Hello API");
        }

        [HttpGet("product")]
        //ActionResult<T>	A wrapper. It does the same as above, but it also tells the compiler (and tools like Swagger): "I am specifically returning a list of Products."
        public async Task<ActionResult<IEnumerable<Product>>> GetFakeData()
        {
            var products = await _fakeDataStore.GetAllProducts();

            if (!products.Any())
                return NoContent();

            return Ok(products);
        }


        [HttpGet("GetProducts")]
        // IActionResult	An interface. It says "I will return an HTTP result" (like 200 OK or 404 Not Found), but it doesn't tell the compiler what data is inside the body.
        public async Task<IActionResult> GetProducts()
        {
            var product = await _mediator.Send(new GetProductsQuery());
            return Ok(product);
        }


        //1. Where does the data come from?

        //   When a client sends an HTTP request, the data can be hidden in different places:

        //   The URL: (e.g., /api/products/5) → [FromRoute]

        //   The Query String: (e.g., /api/products? id = 5) → [FromQuery]

        //   The Request Body: The "invisible" payload of the request → [FromBody]

        //   By using [FromBody], you are telling ASP.NET: "Look at the JSON data inside the body of this HTTP POST request and try to turn it into a Product object."
        //2. How the "Magic" happens (Deserialization)

        //   When the request hits your controller, a background process called a Model Binder sees the[FromBody] tag.

        //    It reads the raw JSON text from the request.

        //    It looks at the Product class properties(e.g., Name, Price).

        //    It matches the JSON keys to your C# properties.

        //    It creates a new instance of Product and passes it into your method as the product parameter.

        [HttpPost("AddProductCommand")]
        public async Task<IActionResult> AddProductCommand([FromBody]Product product)
        {
            await _mediator.Send(new AddProductCommand(product));
            return StatusCode(201);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));

            return Ok(product);
        }

        [HttpPost("notification")]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            var productToReturn = await _mediator.Send(new AddProductCommand(product));

            await _mediator.Publish(new ProductAddedNotification(productToReturn));

            return CreatedAtRoute("GetProductById", new { id = productToReturn.Id }, productToReturn);
        }

    }
}
