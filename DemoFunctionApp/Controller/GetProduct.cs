using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;

namespace DemoFunctionApp.Controller
{
    public class GetProduct
    {
        private readonly AppDbContext _context;

        public GetProduct(AppDbContext context)
        {
            _context = context;
        }

        [Function("GetProduct")]
        public async Task<ActionResult<Product>> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Product/{id}")] HttpRequest req, Guid id)
        {
            var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

            return new OkObjectResult(product);
        }
    }
}
