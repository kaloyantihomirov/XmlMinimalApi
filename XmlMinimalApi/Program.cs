using XmlMinimalApi;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app =  builder.Build();

app.MapGet("/", () => Results.Extensions.Xml<Person>(new Person { 
    FirstName = "Kaloyan",
    LastName = "Kolev" 
}));

app.Run();

public class Person
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}