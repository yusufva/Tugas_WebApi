using Microsoft.EntityFrameworkCore;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.Repositories;
using WebApi.Utilities.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //mengambil connectionstring
builder.Services.AddDbContext<BookingManagementDbContext>(options => options.UseSqlServer(connectionString)); //menginstance db context

// Add Repositories to the Conatainer
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>(); //menginstance University Repo
builder.Services.AddScoped<IRolesRepository, RolesRepository>(); //menginstance Roles Repo
builder.Services.AddScoped<IRoomRepository, RoomRepository>(); //menginstance Room Repo
builder.Services.AddScoped<IEducationRepository, EducationRepository>(); //menginstance Education Repo
builder.Services.AddScoped<IBookingRepository, BookingRepository>(); //menginstance Booking Repo
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>(); //menginstance AccountRole Repo
builder.Services.AddScoped<IAccountsRepository, AccountsRepository>(); //menginstance Accounts Repo
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); //menginstance Employee Repo
builder.Services.AddScoped<GenerateHandler>(); //menginstance Generate Handler

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
