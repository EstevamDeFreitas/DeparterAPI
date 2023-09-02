using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Database;
using Persistence.Repositories.Implementation;
using Persistence.Repositories.Interfaces;
using Services.Services.Implementation;
using Services.Services.Interfaces;
using Services.Utilities;

namespace DeparterTests
{
    public abstract class DeparterBaseTest
    {
        protected DeparterContext departerContext;
        protected IRepositoryWrapper repository;
        protected IServiceWrapper serviceWrapper;

        protected IConfiguration configuration;
        protected IMapper mapper;

        [SetUp]
        public void BaseSetup()
        {
            var options = new DbContextOptionsBuilder<DeparterContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;

            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var mapperConfig = new AutoMapper.MapperConfiguration(new MapperConfig());

            mapper = mapperConfig.CreateMapper();

            departerContext = new DeparterContext(options);
            repository = new RepositoryWrapper(departerContext);
            serviceWrapper = new ServiceWrapper(repository, mapper, configuration);
        }
    }
}