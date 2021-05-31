using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessModels
{
    public class AuditModel
    {
        public AuditModel(string auditId, string description, string functionName, bool? isDeleted, string createdby, DateTime? createdDate, DateTime? modifiedDate)
        {
            AuditId = auditId;
            Description = description;
            FunctionName = functionName;
            IsDeleted = isDeleted;
            Createdby = createdby;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public AuditModel() : this("", "", "", false, "", DateTime.Now, DateTime.Now) { }
        public string AuditId { get; set; }
        public string Description { get; set; }
        public string FunctionName { get; set; }
        public bool? IsDeleted { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
