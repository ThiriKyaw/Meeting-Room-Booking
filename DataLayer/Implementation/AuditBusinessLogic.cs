using BusinessLayer.BusinessModels;
using BusinessLayer.Interface;
using DataLayer.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Implementation
{
    public class AuditBusinessLogic : IAudit
    {
        private readonly MeetingRoomBookingContext _context;
        public AuditBusinessLogic(MeetingRoomBookingContext context)
        {
            _context = context;
        }
        public void CreatAuditLog(AuditModel Log)
        {
            Audit aLog = new Audit();
            aLog.AuditId = Guid.NewGuid().ToString();
            aLog.Description = Log.Description;
            aLog.FunctionName = Log.FunctionName;
            aLog.Createdby = Log.Createdby;
            aLog.IsDeleted = false;
            aLog.CreatedDate = DateTime.Now;
            aLog.ModifiedDate = DateTime.Now;
            _context.Audits.AddAsync(aLog);
            _context.SaveChanges();
        }
    }
}
