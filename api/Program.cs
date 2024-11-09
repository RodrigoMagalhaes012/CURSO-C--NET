using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5000");
var app = builder.Build();
var configuration = builder.Configuration;
ProductRepository.Init(configuration);

app.MapGet("/", () => "Hello World 2");

app.MapPost("products", (Product product) =>
{
    ProductRepository.Add(product);
    return Results.Created($"/products/{product.Code}", product);
});

//api.app.com/users?datastart={date}&dateend={date}
app.MapGet("/products", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return Results.Ok(dateStart + " - " + dateEnd);

});

//api.app.com/user/{code}
app.MapGet("/products/{code}", (string code) =>
{
    var product = ProductRepository.GetBy(code);
    if (product == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(product);
});

app.MapPut("/products", async (HttpContext context) =>
{
    var product = await context.Request.ReadFromJsonAsync<Product>();
    if (product == null)
    {
        return Results.BadRequest();
    }

    var productSaved = ProductRepository.GetBy(product.Code);
    if (productSaved == null)
    {
        return Results.NotFound();
    }

    productSaved.Name = product.Name;
    return Results.Ok(productSaved);
});

app.MapDelete("/products/{code}", ([FromRoute] string code) =>
{
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
    return Results.Ok(productSaved);
});

app.MapGet("/configuration/database", (IConfiguration configuration) =>
{
    return Results.Ok($"{configuration["database:connection"]}/{configuration["database:port"]}");
});


app.Run();

public static class ProductRepository
{
    private static List<Product> Products = new List<Product>();

    public static void Init(IConfiguration configuration)
    {
        var products = configuration.GetSection("products").Get<List<Product>>();
        Products = products;
    }

    public static void Add(Product product)
    {
        if (Products == null)
            Products = new List<Product>();

        Products.Add(product);
    }

    public static List<Product> GetByDateRange(string dateStart, string dateEnd)
    {
        // Implement logic to filter products by date range
        return Products;
    }

    public static Product? GetBy(string code)
    {
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Update(Product product)
    {
        var existingProduct = GetBy(product.Code);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
        }
    }

    public static void Remove(Product product)
    {
        Products.Remove(product);
    }
}

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
}
