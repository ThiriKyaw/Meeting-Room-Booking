using BusinessLayer.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using BusinessLayer.Interface;

namespace MeetingRoomBookingAPI.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MeetingRoomController : ControllerBase
    {

        private readonly ILogger<MeetingRoomController> _logger;
        private readonly IMeetingRoom _meetingRoom;
        private readonly IAudit _audit;
        private readonly IError _error;

        public MeetingRoomController(ILogger<MeetingRoomController> logger , IMeetingRoom meetingRoom, IAudit audit, IError error)
        {
            _logger = logger;
            _meetingRoom = meetingRoom;
            _audit = audit;
            _error = error;

        }

        /// <summary>
        /// Retrive all meeting Rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [Route("/GetAllMeetingRoom")]
        public IActionResult GetAllMeetingRoom()
        {
            try
            {
                _audit.CreatAuditLog(new AuditModel("", "Retrieve All Meeting Room", "GetAllMeetingRoom", false, "System", DateTime.Now, DateTime.Now));
                 List<MeetingRoomModel> meetingRoomList = _meetingRoom.GetAllMeetingRooms();
                 if (meetingRoomList.Count == 0)
                 {
                     return NotFound("There is no User");
                 }
                 return Ok(meetingRoomList);
            }
            catch (Exception ex)
            {
                _error.CreatErrorLog(new ErrorModel("", ex.Message, "GetAllMeetingRoom", ex.InnerException.ToString(), ex.Message, ex.StackTrace, ex.Source, ex.TargetSite.ToString(), false, "System", DateTime.Now, DateTime.Now));
                return BadRequest(ex.Message);
            }
        }
    }
}
