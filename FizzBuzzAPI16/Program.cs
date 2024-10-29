using Application;
using FizzBuzzDomain;
using FizzBuzzDomain.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddDependencies();

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

void AddDependencies() {
	builder.Services.AddSingleton<IFizzBuzzRepo, FizzBuzzRepo>();
	builder.Services.AddSingleton<IRuleManager, RuleManager>();
	builder.Services.AddSingleton<IDapperContext, DapperContext>();
	builder.Services.AddSingleton<IGenerateDefaultRules, GenerateDefaultRules>();
}