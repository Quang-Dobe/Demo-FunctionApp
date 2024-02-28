using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json;

namespace DemoFunctionApp.Controller
{
    public class AddProduct
    {
        private readonly AppDbContext _context;

        public AddProduct(AppDbContext context)
        {
            _context = context;
        }

        [Function("AddProduct")]
        public async Task<ActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonConvert.DeserializeObject<Product>(requestBody);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            return new CreatedResult("/product", product);
        }
    }
}
