using AutoMapper;
using Persistence.Repositories.Implementation;
using Persistence.Repositories.Interfaces;
using Services.Services.Implementation;
using Services.Services.Interfaces;
using Services.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IServiceWrapper, ServiceWrapper>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                        policy =>
                        {
                            policy.AllowAnyOrigin();
                            policy.AllowAnyMethod();
                            policy.AllowAnyHeader();
                        }
                            );
});

var mapperConfig = new AutoMapper.MapperConfiguration(new MapperConfig());

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
