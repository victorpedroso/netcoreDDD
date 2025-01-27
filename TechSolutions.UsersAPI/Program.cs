using TechSolutions.Infrastructure.Data.Middlewares;
using TechSolutions.Infrastructure.IoC;
using WebHostCustomization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddHttpClient();

builder.Services.AddSecurityServices(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseServiceApplicationAuth();

app.MapControllers();

app.Run();
