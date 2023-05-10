using Application;
using Infrastructure;
using Infrastructure.Persistence.DbInitializer;
using Presentation;
using Application.Common.Extensions;
using Watt.Backend.Api.Extensions;
using FluentValidation.AspNetCore;
using System.Reflection;
using Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add(typeof(CustomExceptionFilter));
        options.ModelValidatorProviders.Clear();
    })
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssembly(Assembly.Load(Assembly.GetExecutingAssembly()
            .GetReferencedAssemblies().FirstOrDefault(assembly =>
                assembly.Name.Equals("Application"))
            ?? throw new InvalidOperationException()));
        fv.ImplicitlyValidateChildProperties = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation()
    .ConfigPipelineBehavior()
    .ConfigProfileMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

try
{
    using var scope = app.Services.CreateScope();

    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dbInitializer.InitializeAsync();
}
catch (Exception e)
{
    throw e;
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
