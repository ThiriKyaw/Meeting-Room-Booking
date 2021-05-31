using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessModels
{
    public class ErrorModel
    {
        public ErrorModel(string errorLogId, string description, string functionName, string innerException, string message, string stackTrace, string source, string targetSite, bool? isDeleted, string createdby, DateTime? createdDate, DateTime? modifiedDate)
        {
            ErrorLogId = errorLogId;
            Description = description;
            FunctionName = functionName;
            InnerException = innerException;
            Message = message;
            StackTrace = stackTrace;
            Source = source;
            TargetSite = targetSite;
            IsDeleted = isDeleted;
            Createdby = createdby;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public ErrorModel() : this("", "", "", "", "", "", "", "", false, "", DateTime.Now, DateTime.Now) { }
        public string ErrorLogId { get; set; }
        public string Description { get; set; }
        public string FunctionName { get; set; }
        public string InnerException { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
        public bool? IsDeleted { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
