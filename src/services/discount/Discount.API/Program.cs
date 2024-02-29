using Discount.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();
var app = builder.Build().MigrateDatabase().ConfigurePipeline();



app.Run();
