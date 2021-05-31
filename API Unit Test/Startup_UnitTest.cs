using MeetingRoomBookingAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;



namespace API_Unit_Test
{
    [TestClass]
    public class Startup_UnitTest
    {
        private Mock<IConfiguration> _ConfigurationStub;
        private IServiceCollection _services;

        [TestInitialize]
        public void Setup()
        {

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.APItest.json").Build();
            _ConfigurationStub = new Mock<IConfiguration>();
            _services = new ServiceCollection();

            _ConfigurationStub.Setup(x => x.GetSection("ServiceUrls")).Returns(config.GetSection("ServiceUrls"));

        }

        [TestMethod]
        public void ConfigureSercies_UnitTest() 
        {
            var target = new Startup(_ConfigurationStub.Object);

            target.ConfigureServices(_services);

            var serviceProvider = _services.BuildServiceProvider();

            var _IConfigruationService = _services.BuildServiceProvider();
            Assert.IsNotNull(_IConfigruationService);

        
        }
    }
}
