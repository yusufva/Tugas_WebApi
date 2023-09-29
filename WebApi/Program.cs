using Microsoft.EntityFrameworkCore;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //mengambil connectionstring
builder.Services.AddDbContext<BookingManagementDbContext>(options => options.UseSqlServer(connectionString)); //menginstance db context

// Add Repositories to the Conatainer
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
