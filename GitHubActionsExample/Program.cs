var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

// WebApplicationFactory<Program>'ın (integration testleri) uygulamayı
// başlatabilmesi için Program sınıfını açığa çıkarır. Davranışı değiştirmez.
public partial class Program { }