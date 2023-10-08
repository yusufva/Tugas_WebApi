using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApi.Contracts;
using WebApi.Data;
using Microsoft.IdentityModel.Tokens;
using WebApi.DTOs.Employees;
using WebApi.Repositories;
using WebApi.Utilities.Handler;
using WebApi.Utilities.Validations.Employees;
using System.Text;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.OpenApi.Models;

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

// Add Handler Service to Controller
builder.Services.AddTransient<IEmailHandler, EmailHandler>(_ => new EmailHandler(
        builder.Configuration["SmtpService:Server"],
        int.Parse(builder.Configuration["SmtpService:Port"]),
        builder.Configuration["SmtpService:FromEmailAddress"]
    ));
builder.Services.AddScoped<ITokenHandler, WebApi.Utilities.Handler.TokenHandler>();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage);

            return new BadRequestObjectResult(new ResponseValidatorHandler(errors));
        };
    });

//Add Fluent validator
builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//JWT Authentication Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; //dev only
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWTService:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWTService:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTService:SecretKey"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

//add cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
policy.AllowAnyHeader();
policy.WithMethods("GET", "POST", "DELETE", "PUT", "OPTIONS");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(x => {
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Metrodata Coding Camp",
        Description = "ASP.NET Core API 6.0"
    });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
