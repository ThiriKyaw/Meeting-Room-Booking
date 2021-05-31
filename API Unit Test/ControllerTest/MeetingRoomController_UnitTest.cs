using BusinessLayer.Interface;
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
using System;
using System.Collections.Generic;
using System.Net.Http;
using static API_Unit_Test.SampleUnitTestVariablesAndMethods;

namespace API_Unit_Test.ControllerTest
{
    [TestClass]
    public class MeetingRoomController_UnitTest
    {
        private MeetingRoomController _controller;
        private ILogger<MeetingRoomController> _meetingRoomLogger;
        private MeetingRoomBookingContext _contextMemory;
        private DbContextOptions<MeetingRoomBookingContext> optionsMemory;
        private MeetingRoomBusinessLogic _meetingRoomBusinessLogic;
        private AuditBusinessLogic _auditBusinessLogic;
        private ErrorBusinessLogic  _errorBusinessLogic;


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
            _meetingRoomLogger = factory.CreateLogger<MeetingRoomController>();
            _contextMemory = new MeetingRoomBookingContext(optionsMemory);
            _auditBusinessLogic = new AuditBusinessLogic(_contextMemory);
            _errorBusinessLogic = new ErrorBusinessLogic(_contextMemory);
            _meetingRoomBusinessLogic = new MeetingRoomBusinessLogic(_contextMemory);
            
        }

        [TestCleanup]
        public void Cleanup()
        {
            Environments.removeAllEnvironmentVariables();
        }

        [TestMethod]
        public void Test_GetAllMeetingRoom()
        {
            List<MeetingRoom> meetingRoomList = GenerateMockMeetingRoomList();
            List<Audit> auditList = GenerateMockAuditList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(meetingRoomList, _contextMemory);
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(auditList, _contextMemory);
            _contextMemory.SaveChanges();
            GenerateController();
            var result = _controller.GetAllMeetingRoom();
            Assert.IsNotNull(result);

        }
        public List<MeetingRoom> GenerateMockMeetingRoomList()
        {
            MeetingRoom meetingRoom = new MeetingRoom();
            meetingRoom.MeetingRoomId = "meetingroom12";
            meetingRoom.MeetingRoomName = "meeting room 1";
            meetingRoom.MeetingRoomMaxSize = "5";
            meetingRoom.HaveProjector = true;
            meetingRoom.HaveTv = true;
            meetingRoom.Createdby = "user 1";
            meetingRoom.IsDeleted = false;

            List<MeetingRoom> meetingRoomList = new List<MeetingRoom>();
            meetingRoomList.Add(meetingRoom);
            return meetingRoomList;
        }

        public List<Audit> GenerateMockAuditList()
        {
            Audit aLog = new Audit();
            aLog.AuditId = Guid.NewGuid().ToString();
            aLog.Description ="test";
            aLog.FunctionName = "test";
            aLog.Createdby = "test";
            aLog.IsDeleted = false;
            aLog.CreatedDate = DateTime.Now;
            aLog.ModifiedDate = DateTime.Now;

            List<Audit> AuditList = new List<Audit>();
            AuditList.Add(aLog);
            return AuditList;
        }

        public void GenerateController()
        {
            var httpContext = new DefaultHttpContext();

            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            _controller = new MeetingRoomController(_meetingRoomLogger, _meetingRoomBusinessLogic, _auditBusinessLogic,_errorBusinessLogic)
            {
                ControllerContext = controllerContext,

            };
        }
    }
}
