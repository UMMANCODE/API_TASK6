using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TASK3_Business.Exceptions;
using TASK3_Business.Profiles;
using TASK3_Business.Services.Implementations;
using TASK3_Business.Services.Interfaces;
using TASK3_Business.Validators.StudentValidators;
using TASK3_DataAccess;
using TASK3_DataAccess.Repositories.Implementations;
using TASK3_DataAccess.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => {
  options.InvalidModelStateResponseFactory = context => {
    var errors = context.ModelState.Where(x => x.Value.Errors.Count > 0)
    .Select(x => new RestExceptionError(x.Key, x.Value.Errors.First().ErrorMessage)).ToList();
    return new BadRequestObjectResult(new { message = "", errors });
  };
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton(provider => new MapperConfiguration(cfg => {
  cfg.AddProfile(new MapProfile(provider.GetService<IHttpContextAccessor>()!));
}).CreateMapper());

builder.Services.AddDbContext<AppDbContext>(option => {
  option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Fluent Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<StudentCreateOneDtoValidator>();

//Custom Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();

//Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => {
  loggerConfiguration
  .ReadFrom.Configuration(hostingContext.Configuration);
});

//Microelements
builder.Services.AddFluentValidationRulesToSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();

