using DataLayer.DBModels;
using DataLayer.Implementation;
using MeetingRoomBookingAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http;
using static API_Unit_Test.SampleUnitTestVariablesAndMethods;

namespace API_Unit_Test.ControllerTest
{
    [TestClass]
    public class UserController_UnitTest
    {
        private HttpClient _apiClient;
        private UserController _controller;
        private ILogger<UserController> _userLogger;
        private MeetingRoomBookingContext _contextMemory;
        private DbContextOptions<MeetingRoomBookingContext> optionsMemory;
        private UserBusinessLogic _userBusinessLogic;
        private AuditBusinessLogic _auditBusinessLogic;
        private ErrorBusinessLogic _errorBusinessLogic;

        [TestInitialize]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.APItest.json")
                .Build();

            var svvc = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var builderMemory = new DbContextOptionsBuilder<MeetingRoomBookingContext>()
            .UseInMemoryDatabase(databaseName: "UnitTestDB")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            optionsMemory = builderMemory.Options;

            var factory = svvc.GetService<ILoggerFactory>();
            _userLogger = factory.CreateLogger<UserController>();
            _contextMemory = new MeetingRoomBookingContext(optionsMemory);
            _userBusinessLogic = new UserBusinessLogic(_contextMemory);
            _auditBusinessLogic = new AuditBusinessLogic(_contextMemory);
            _errorBusinessLogic = new ErrorBusinessLogic(_contextMemory);
            _apiClient = new HttpClient();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Environments.removeAllEnvironmentVariables();
        }
        [TestMethod]
        public void Test_GetAllUser() 
        {
            List<User> userlist = GenerateMockUserList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(userlist, _contextMemory);
            _contextMemory.SaveChanges();
            GenerateController();
            var result = _controller.GetAllUser();
            Assert.IsNotNull(result);

        }
        public List<User> GenerateMockUserList() 
        {
            User userModel = new User();
            userModel.UserId = "test";
            userModel.LoginName = "test";
            userModel.Email = "test";
            userModel.Createdby = "test";
            userModel.Name = "test";
            userModel.IsDeleted = false;
            List<User> userlist = new List<User>();
            userlist.Add(userModel);
            return userlist;
        }

        public void GenerateController()
        {
            var httpContext = new DefaultHttpContext();
                                                                               
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            _controller = new UserController(_userLogger, _userBusinessLogic, _auditBusinessLogic,_errorBusinessLogic)
            {
                ControllerContext = controllerContext,
            };
        }




    }
}
