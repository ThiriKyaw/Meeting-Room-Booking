using MeetingRoomBookingAPI.BusinessLogic.Helper;
using BusinessLayer.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BusinessLayer.Interface;
using Microsoft.Extensions.Logging;
using DataLayer.DBModels;

namespace MeetingRoomBookingAPI.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MeetingEventController : ControllerBase
    {
        private readonly ILogger<MeetingEventController> _logger;
        private readonly IMeetingEvent _meetingEvent;
        private readonly IAudit _audit;
        private readonly IError _error;

        public MeetingEventController(ILogger<MeetingEventController> logger, IMeetingEvent meetingEvent , IAudit audit, IError error)
        {
            _logger = logger;
            _meetingEvent = meetingEvent;
            _audit = audit;
            _error = error;

        }

        /// <summary>
        /// To Get the Meeting Events
        /// </summary>
        /// <param name="meetingeventMdl"></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [Route("/GetAllMeetingEvents")]
        public IActionResult GetAllMeetingEvents()
        {
            try
            {
                     _audit.CreatAuditLog(new AuditModel("","Retrieve All Meeting Events", "GetAllMeetingEvents" ,false, "System",DateTime.Now, DateTime.Now));
                    List<MeetingEventModel> meetingEventMdlList = _meetingEvent.GetAllMeetingEvent();
                    return Ok(meetingEventMdlList);
                
            }
            catch (Exception ex)
            {
                _error.CreatErrorLog(new ErrorModel("", ex.Message, "GetAllMeetingEvents", ex.InnerException.ToString(), ex.Message, ex.StackTrace, ex.Source, ex.TargetSite.ToString(), false, "System", DateTime.Now, DateTime.Now));
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// To Get the Meeting Events
        /// </summary>
        /// <param name="meetingeventMdl"></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [Route("/GetMeetingEventsByRoomId/{roomId}")]
        public IActionResult GetMeetingEventsByRoomId(string roomId)
        {
            try
            {
                _audit.CreatAuditLog(new AuditModel("", "Retrieve All Meeting Events by RoomId", "GetMeetingEventsByRoomId", false, "System", DateTime.Now, DateTime.Now));
                      List<MeetingEventModel> meetingEventMdlList = _meetingEvent.GetMeetingListByRoomId(roomId);
                      return Ok(meetingEventMdlList);
                
            }
            catch (Exception ex)
            {
                _error.CreatErrorLog(new ErrorModel("", ex.Message, "GetMeetingEventsByRoomId", ex.InnerException.ToString(), ex.Message, ex.StackTrace, ex.Source, ex.TargetSite.ToString(), false, "System", DateTime.Now, DateTime.Now));
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// To Booking the Meeting Room
        /// </summary>
        /// <param name="meetingeventMdl"></param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1")]
        [Route("/BookMeetingRoom")]
        public IActionResult BookMeetingRoom([FromBody] MeetingEventModel meetingeventMdl)
        {
            try
            {
                _audit.CreatAuditLog(new AuditModel("", "Book Meeting Room", "BookMeetingRoom", false, "System", DateTime.Now, DateTime.Now));

                MeetingEventModel result = _meetingEvent.CreateMeetingEvent(meetingeventMdl);
                if (!String.IsNullOrEmpty(result.EventId))
                {
                    if (result.EventId == "1") 
                    {
                        return BadRequest(result);
                    }
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _error.CreatErrorLog(new ErrorModel("", ex.Message, "BookMeetingRoom", ex.InnerException.ToString(), ex.Message, ex.StackTrace, ex.Source, ex.TargetSite.ToString(), false, "System", DateTime.Now, DateTime.Now));
                return Forbid(ex.Message);
            }
        }

        /// <summary>
        /// To Update the Booking
        /// </summary>
        /// <param name="meetingeventMdl"></param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1")]
        [Route("/UpdateBooking")]
        public IActionResult UpdateBooking([FromBody] MeetingEventModel meetingeventMdl)
        {
            try
            {
                _audit.CreatAuditLog(new AuditModel("", "Update Booking", "UpdateBooking", false, "System", DateTime.Now, DateTime.Now));
                      string result = _meetingEvent.UpdateMeetingEvent(meetingeventMdl);
                      if (result == Constants.bookingupdateSuccess)
                      {
                          return Ok(Constants.bookingupdateSuccess);
                      }
                      return BadRequest(result);
               
            }
            catch (Exception ex)
            {
                _error.CreatErrorLog(new ErrorModel("", ex.Message, "UpdateBooking", ex.InnerException.ToString(), ex.Message, ex.StackTrace, ex.Source, ex.TargetSite.ToString(), false, "System", DateTime.Now, DateTime.Now));
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// To Cancel the Booking
        /// </summary>
        /// <param name="meetingeventMdl"></param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1")]
        [Route("/CancelBooking")]
        public IActionResult CancelBooking([FromBody] MeetingEventModel meetingeventMdl)
        {
            try
            {
                 _audit.CreatAuditLog(new AuditModel("", "Cancel Booking", "CancelBooking", false, "System", DateTime.Now, DateTime.Now));
                string result = _meetingEvent.DeleteMeetingEvent(meetingeventMdl);
                 if (result == Constants.bookingDeleteSuccess)
                 {
                     return Ok(Constants.bookingDeleteSuccess);
                 }
                 return BadRequest(result);
            }
            catch (Exception ex)
            {
                _error.CreatErrorLog(new ErrorModel("", ex.Message, "CancelBooking", ex.InnerException.ToString(), ex.Message, ex.StackTrace, ex.Source, ex.TargetSite.ToString(), false, "System", DateTime.Now, DateTime.Now));
                return BadRequest(ex.Message);
            }
        }
    }
}
