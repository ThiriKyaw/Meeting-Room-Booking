using BusinessLayer.BusinessModels;
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
using System.Linq;
using System.Net.Http;
using static API_Unit_Test.SampleUnitTestVariablesAndMethods;

namespace API_Unit_Test.ControllerTest
{
    [TestClass]
    public class MeetingEventController_UnitTest
    {
        private HttpClient _apiClient;
        private MeetingEventController _controller;
        private ILogger<MeetingEventController> _meetingEventLogger;
        private MeetingRoomBookingContext _contextMemory;
        private DbContextOptions<MeetingRoomBookingContext> optionsMemory;
        private MeetingEventBusinessLogic _meetingEventBusinessLogic;
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
            _meetingEventLogger = factory.CreateLogger<MeetingEventController>();
            _contextMemory = new MeetingRoomBookingContext(optionsMemory);
            _meetingEventBusinessLogic = new MeetingEventBusinessLogic(_contextMemory);
            _auditBusinessLogic = new AuditBusinessLogic(_contextMemory);
            _errorBusinessLogic = new ErrorBusinessLogic(_contextMemory);
            _apiClient = new HttpClient();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Environments.removeAllEnvironmentVariables();
        }
        #region
        [TestMethod]
        public void Test_GetAllMeetingEvent()
        {
            List<MeetingEvent> meetingEventList = GenerateMockMeetingEventList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(meetingEventList, _contextMemory);
            List<User> userList = GenerateMockUserList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(userList, _contextMemory);
            List<MeetingRoom> meetingRoomList = GenerateMockMeetingRoomList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(meetingRoomList, _contextMemory);
            _contextMemory.SaveChanges();
            GenerateController();
            var result = _controller.GetAllMeetingEvents();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.RemoveAllSampleData(_contextMemory);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_GetMeetingEventsByRoomID()
        {
            List<MeetingEvent> meetingEventList = GenerateMockMeetingEventList();
            List<MeetingRoom> meetingRoomList = GenerateMockMeetingRoomList();
            List<User> userList = GenerateMockUserList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(meetingEventList, _contextMemory);
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(userList, _contextMemory);
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(meetingRoomList, _contextMemory);
            _contextMemory.SaveChanges();
            GenerateController();
            var result = _controller.GetMeetingEventsByRoomId("meetingroom1");
            SampleUnitTestVariablesAndMethods.MemoryDatabase.RemoveAllSampleData(_contextMemory);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_CreateBooking()
        {
           
            List<User> userList = GenerateMockUserList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(userList, _contextMemory);
            List<MeetingRoom> meetingRoomList = GenerateMockMeetingRoomList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(meetingRoomList, _contextMemory);
            List<MeetingEventModel> meetingEventModelList = GenerateMockMeetingEventModelList();
            _contextMemory.SaveChanges();
            GenerateController();
            var result = _controller.BookMeetingRoom(meetingEventModelList.FirstOrDefault());
            SampleUnitTestVariablesAndMethods.MemoryDatabase.RemoveAllSampleData(_contextMemory);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_UpdateBooking()
        {
            List<MeetingEvent> meetingEventList = GenerateMockMeetingEventList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(meetingEventList, _contextMemory);
            List<User> userList = GenerateMockUserList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(userList, _contextMemory);
            List<MeetingRoom> meetingRoomList = GenerateMockMeetingRoomList();
            List<MeetingEventModel> meetingEventModelList = GenerateMockMeetingEventModelList();
            _contextMemory.SaveChanges();
            GenerateController();
            var result = _controller.UpdateBooking(meetingEventModelList.FirstOrDefault());
            SampleUnitTestVariablesAndMethods.MemoryDatabase.RemoveAllSampleData(_contextMemory);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_CancelBooking ()
        {
            List<MeetingEvent> meetingEventList = GenerateMockMeetingEventList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(meetingEventList, _contextMemory);
            List<User> userList = GenerateMockUserList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(userList, _contextMemory);
            List<MeetingRoom> meetingRoomList = GenerateMockMeetingRoomList();
            SampleUnitTestVariablesAndMethods.MemoryDatabase.CreateSampleData(meetingRoomList, _contextMemory);
            List<MeetingEventModel> meetingEventModelList = GenerateMockMeetingEventModelList();
            _contextMemory.SaveChanges();
            GenerateController();
            var result = _controller.CancelBooking(meetingEventModelList.FirstOrDefault());
            SampleUnitTestVariablesAndMethods.MemoryDatabase.RemoveAllSampleData(_contextMemory);
            Assert.IsNotNull(result);
        }
        #endregion



        public List<MeetingEvent> GenerateMockMeetingEventList()
        {
            MeetingEvent meetingEvent = new MeetingEvent();

            meetingEvent.EventId = "TestEvent1";
            meetingEvent.EventName = "Test Event1";
            meetingEvent.EventDescription = "Test 1";
            meetingEvent.MeetingRoomId = "meetingroom1";
            meetingEvent.EventStartDateTime = DateTime.Now;
            meetingEvent.EventEndDateTime = DateTime.Now;
            meetingEvent.EventCreatedby = "testuser";
            meetingEvent.TotalPeopleCount = 5;
            meetingEvent.IsDeleted = false;
            List<MeetingEvent> meetingEventList = new List<MeetingEvent>();
            meetingEventList.Add(meetingEvent);
            return meetingEventList;
        }

        public List<MeetingEventModel> GenerateMockMeetingEventModelList()
        {
            MeetingEventModel meetingEvent = new MeetingEventModel();

            meetingEvent.EventId = "TestEvent1";
            meetingEvent.EventName = "Test Event1";
            meetingEvent.EventDescription = "Test 1";
            meetingEvent.MeetingRoomId = "meetingroom1";
            meetingEvent.EventStartDateTime = DateTime.Now;
            meetingEvent.EventEndDateTime = DateTime.Now;
            meetingEvent.EventCreatedby = "testuser";
            meetingEvent.TotalPeopleCount = 5;
            meetingEvent.IsDeleted = false;
            List<MeetingEventModel> meetingEventList = new List<MeetingEventModel>();
            meetingEventList.Add(meetingEvent);
            return meetingEventList;
        }

        public List<User> GenerateMockUserList()
        {
            User userModel = new User();
            userModel.UserId = "testuser";
            userModel.LoginName = "test user";
            userModel.Email = "test user";
            userModel.Createdby = "test user";
            userModel.Name = "test user";
            userModel.IsDeleted = false;
            List<User> userlist = new List<User>();
            userlist.Add(userModel);
            return userlist;
        }

        public List<MeetingRoom> GenerateMockMeetingRoomList()
        {
            MeetingRoom meetingRoom = new MeetingRoom();

            meetingRoom.MeetingRoomId = "meetingroom1";
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

        public void GenerateController()
        {
            var httpContext = new DefaultHttpContext();

            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            _controller = new MeetingEventController(_meetingEventLogger, _meetingEventBusinessLogic, _auditBusinessLogic, _errorBusinessLogic)
            {
                ControllerContext = controllerContext,

            };
        }
    }
}
