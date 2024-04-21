using AM.Cars.Server.Infrustructure.Database.Context;
using AM.Cars.Server.Infrustructure.Database.Extensions;
using AM.Cars.Server.Infrustructure.Extensions;
using AM.Cars.Server.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrustructureDatabaseBuilders();
builder.Services.AddConfigDbContext();
builder.Services.AddRepositories();
builder.Services.AddAutoMapperProfiles();
builder.Services.AddInfrustructureUtilities();
builder.Services.AddInfrustructureServices();
builder.Services.AddValidators();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseMiddleware<ErrorHandlingMiddleware>();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

AddMigrations(app);

app.Run();

void AddMigrations(WebApplication webApplication)
{
    using var scope = webApplication.Services.CreateScope();

    try
    {
        var context = scope.ServiceProvider.GetService<ConfigContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, ex.Message);
    }
}
