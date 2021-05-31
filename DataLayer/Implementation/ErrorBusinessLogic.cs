using BusinessLayer.BusinessModels;
using BusinessLayer.Interface;
using DataLayer.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Implementation
{
    public class ErrorBusinessLogic : IError
    {
        private readonly MeetingRoomBookingContext _context;
        public ErrorBusinessLogic(MeetingRoomBookingContext context)
        {
            _context = context;
        }
        public void CreatErrorLog(ErrorModel Log)
        {
            ErrorLog aLog = new ErrorLog();
            aLog.ErrorLogId = Guid.NewGuid().ToString();
            aLog.Description = Log.Description;
            aLog.FunctionName = Log.FunctionName;
            aLog.Createdby = Log.Createdby;
            aLog.IsDeleted = false;
            aLog.CreatedDate = DateTime.Now;
            aLog.ModifiedDate = DateTime.Now;
            aLog.InnerException = Log.InnerException;
            aLog.Source = Log.Source;
            aLog.StackTrace = Log.StackTrace;
            aLog.TargetSite = Log.TargetSite;
            aLog.Message = Log.Message;
            _context.ErrorLogs.AddAsync(aLog);
            _context.SaveChanges();
           
          
        }
    }
}
