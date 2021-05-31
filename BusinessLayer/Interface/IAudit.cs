using BusinessLayer.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAudit
    {
        public  void CreatAuditLog(AuditModel Log);
    }
}
