using EFCoreMappingApp;
using ReservationsManager.BLL;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.BLL.Services;
using ReservationsManager.DAL.Interfaces;
using ReservationsManager.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("myDb1");
builder.Services.AddSingleton(x => new RezervationsDbContext(connectionString));

//Repositories
builder.Services.AddScoped<IActionsRepository, ActionsRepository>();
builder.Services.AddScoped<IReservationsRepository, ReservationsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IActionEmployeesRepository, ActionEmployeesRepository>();
builder.Services.AddScoped<ITimeBlocksRepository, TimeBlocksRepository>();

//Services
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IActionEmployeesService, ActionEmployeesService>();
builder.Services.AddScoped<IReservationsService, ReservationsService>();

builder.Services.AddAutoMapper(typeof(BllAssemblyMarker));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }