using FridgeRegistry.Identity.Common.Initializations;
using FridgeRegistry.Identity.Common.Middlewares;
using FridgeRegistry.Identity.Services.Identity;
using FridgeRegistry.Identity.Services.Jwt;
using static FridgeRegistry.Identity.Common.Initializations.RolesInitialization;

var builder = WebApplication.CreateBuilder(args);

// Services initialization
builder.Services.AddMvc();
builder.Services.AddCustomValidation();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddIdentityDbContext(builder.Configuration);

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddCustomCors();
builder.Services.AddSwaggerDocsGen();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilterMiddleware>();
});

var app = builder.Build();

// Middlewares && on application start static methods

await InitializeRoles(app, builder.Configuration);

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();