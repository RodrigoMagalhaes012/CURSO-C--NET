using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World 2");
app.MapPost("/user", () => new { Name = "Rodrigo Amorim", Age = 25 });
app.MapGet("/AddHeader", (HttpResponse response) =>
{
    response.Headers.Add("teste", "Rodrigo Amorim");
    return new { Name = "Rodrigo Amorim", Age = 25 };
});

app.MapPost("saveproduct", (Product product) =>
{
    return product.Code + " - " + product.Name;
});


//api.app.com/users?datastart={date}&dateend={date}
app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return dateStart + " - " + dateEnd;
});
//api.app.com/user/{code}
app.MapGet("/getproduct/{code}", ([FromRoute] string code) =>
{
    return code;
});

app.Run();

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
}
