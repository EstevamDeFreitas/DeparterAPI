using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence.Database;
using Persistence.Repositories.Implementation;
using Persistence.Repositories.Interfaces;
using Services.Services.Implementation;
using Services.Services.Interfaces;
using Services.Utilities;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IServiceWrapper, ServiceWrapper>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Departer API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Example: 'Bearer abc...'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }, Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
});


builder.Services.AddDbContext<DeparterContext>(options =>
{
    options.UseNpgsql(configuration["ConnectionStrings:DbConnection"]);
});


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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
