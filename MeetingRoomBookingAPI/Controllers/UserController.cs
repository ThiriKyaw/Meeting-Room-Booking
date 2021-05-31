using BusinessLayer.BusinessModels;
using DataLayer.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetingRoomBookingAPI.Controllers
{
  
    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUser _userBusinessLogic;
        private readonly IAudit _audit;
        private readonly IError _error;

        public UserController(ILogger<UserController> logger, IUser userBusinessLogic, IAudit audit, IError error)
        {
            _logger = logger;
            _userBusinessLogic = userBusinessLogic;
            _audit = audit;
            _error = error;


        }
        /// <summary>
        /// Get All User 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [Route("/getalluser")]
        public IActionResult GetAllUser()
        {
            try
            {
                _audit.CreatAuditLog(new AuditModel("", "Get All User", "GetAllUser", false, "System", DateTime.Now, DateTime.Now));
                List<UserModel> userList = _userBusinessLogic.GetAllUser();
                if (userList.Count == 0) {
                    return NotFound("There is no User");
                }
                return Ok(userList);
            }
            catch (Exception ex)
            {
                _error.CreatErrorLog(new ErrorModel("", ex.Message, "GetAllUser", ex.InnerException.ToString(), ex.Message, ex.StackTrace, ex.Source, ex.TargetSite.ToString(), false, "System", DateTime.Now, DateTime.Now));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [MapToApiVersion("2")]
        [Route("/getalluser")]
        public IActionResult GetAllUserV2()
        {
            try
            {
                List<UserModel> userList = _userBusinessLogic.GetAllUser();
                if (userList.Count == 0)
                {
                    return NotFound("There is no User");
                }
                return Ok("There is Version 2 API");
            }
            catch (Exception ex)
            {
                _error.CreatErrorLog(new ErrorModel("", ex.Message, "GetAllUserV2", ex.InnerException.ToString(), ex.Message, ex.StackTrace, ex.Source, ex.TargetSite.ToString(), false, "System", DateTime.Now, DateTime.Now));
                return BadRequest(ex.Message);
            }
        }





    }
}
