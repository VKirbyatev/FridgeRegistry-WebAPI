using System.Text;
using System.Text.Json.Serialization;
using FridgeRegistry.Application;
using FridgeRegistry.Infrastructure;
using FridgeRegistry.WebAPI.Common.Initializations;
using FridgeRegistry.WebAPI.Middlewares.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCustomCors();
builder.Services.AddCustormSwagger();

builder.Services
    .AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

public partial class WebApiProgram {}