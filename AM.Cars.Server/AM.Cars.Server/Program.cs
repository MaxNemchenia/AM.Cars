using AM.Cars.Server.Infrustructure.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrustructureDatabaseBuilders();
builder.Services.AddConfigDbContext();
builder.Services.AddRepositories();

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

app.Run();
