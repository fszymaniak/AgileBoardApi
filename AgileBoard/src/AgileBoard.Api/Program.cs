using AgileBoard.Application.ExceptionHandling;
using AgileBoard.Core.Installers;
using AgileBoard.Infrastructure.Extensions;
using AgileBoard.Application.Extensions;
using AgileBoard.Core.Extensions;
using AgileBoard.Infrastructure.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services
    .AddApplication()
    .AddCore();

builder.AddInfrastructre();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.InstallServicesInAssembly();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<Clock, Clock>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("foo");
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
