using System.Text;
using System.Text.Json.Serialization;
using FridgeRegistry.Application;
using FridgeRegistry.Infrastructure;
using FridgeRegistry.Infrastructure.Persistence;
using FridgeRegistry.WebAPI.Common.HealthChecks;
using FridgeRegistry.WebAPI.Common.Initializations;
using FridgeRegistry.WebAPI.Contracts.Responses;
using FridgeRegistry.WebAPI.Middlewares.Exceptions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCaching(builder.Configuration);

builder.Services.AddCustomCors();
builder.Services.AddCustomSwagger();

builder.Services
    .AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

builder.Services.AddHealthChecks()
    .AddCheck<RedisHealthCheck>("redis")
    .AddCheck(
        "database-check",
        new PostgresConnectionHealthCheck(builder.Configuration.GetConnectionString("DbConnection")),
        HealthStatus.Unhealthy,
        new string[] { "fridge-registry-db" }
    );

var app = builder.Build();


app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var response = new HealthCheckResponse()
        {
            Status = report.Status.ToString(),
            Checks = report.Entries.Select(x => new HealthCheck()
            {
                Component = x.Key,
                Status = x.Value.Status.ToString(),
                Description = x.Value.Description,
            }),

            Duration = report.TotalDuration
        };

        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
});

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FridgeRegistryDbContext>();
    await context.Database.MigrateAsync();
}

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