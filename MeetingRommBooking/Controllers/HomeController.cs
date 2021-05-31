using MeetingRommBooking.Helper.Common;
using MeetingRommBooking.Models;
using BusinessLayer.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MeetingRoomBookingAPI.BusinessLogic.Helper;

namespace MeetingRommBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync(string meetingroomid)
        {
            ConnectToAPI connectToApi = new ConnectToAPI();
            List<MeetingRoomModel> meetingRoomlist= await connectToApi.GetAllMeetingRoomAsync();
            ViewBag.CurrentUserId = "589bd67d-b248-44d5-9b82-dc2e012f787f";
            ViewBag.meetingRoomList = meetingRoomlist;
            List<MeetingEventModel> meetingEventList = new List<MeetingEventModel>();
            if (meetingroomid == null)
            {
                ViewBag.selectedMeetingRoomId = meetingRoomlist.FirstOrDefault().MeetingRoomId;
                meetingEventList = await connectToApi.GetEventsbyRoomIdAsync(meetingRoomlist.FirstOrDefault().MeetingRoomId);
            }
            else {
                ViewBag.selectedMeetingRoomId = meetingroomid;
                meetingEventList = await connectToApi.GetEventsbyRoomIdAsync(meetingroomid);
            }
            return View(meetingEventList);
        }

        [HttpPost]
        [Route("home/BookMeetingAsync")]
        public async Task<JsonResult> BookMeetingAsync(MeetingEventModel meetingEventModel)
        {
            ConnectToAPI connectToApi = new ConnectToAPI();
            MeetingEventModel result = await connectToApi.BookMeeting(meetingEventModel);
            if (result == null) 
            {
                return Json(Constants.Error);
            }
            else if (String.IsNullOrEmpty(result.EventId)) 
            {
                return Json(Constants.bookingOverLapError);
            }
            else if (result.EventId == "1")
            {
                return Json(Constants.BookingTimeError);
            }
            return Json(Constants.bookingSuccess);
        }

        [HttpPost]
        [Route("home/UpdateMeetingAsync")]
        public async Task<JsonResult> UpdateMeetingAsync(MeetingEventModel meetingEventModel)
        {
            if (meetingEventModel.EventStartDateTime < DateTime.Now) 
            {
                return Json(Constants.BookingEditError);
            }
            ConnectToAPI connectToApi = new ConnectToAPI();
            string result = await connectToApi.UpdateMeeting(meetingEventModel);
            if (result == null)
            {
                return Json(Constants.Error);
            }
            return Json(result);
        }

        [HttpGet]
        [Route("home/CancelBookingAsync")]
        public async Task<JsonResult> CancelMeetingEvent(string meetingEventID)
        {
            ConnectToAPI connectToApi = new ConnectToAPI();
            MeetingEventModel meetingEventModel = new MeetingEventModel();
            meetingEventModel.EventId = meetingEventID;
          string result = await connectToApi.CancelMeeting(meetingEventModel);
            return Json(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
