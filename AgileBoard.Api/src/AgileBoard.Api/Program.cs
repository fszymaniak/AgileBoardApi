using AgileBoard.Api.Clock;
using AgileBoard.Api.ExceptionHandling;
using AgileBoard.Api.Installers;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.InstallServicesInAssembly();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IClock, Clock>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
