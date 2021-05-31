using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessModels
{
    public class MeetingRoomModel
    {
       
        public MeetingRoomModel() : this("", "", "",false, false, false, "", DateTime.Now, DateTime.Now) { }

        public MeetingRoomModel(string meetingRoomId, string meetingRoomName, string meetingRoomMaxSize, bool haveProjector, bool haveTv, bool isDeleted, string createdby, DateTime? createdDate, DateTime? modifiedDate)
        {
            MeetingRoomId = meetingRoomId;
            MeetingRoomName = meetingRoomName;
            MeetingRoomMaxSize = meetingRoomMaxSize;
            HaveProjector = haveProjector;
            HaveTv = haveTv;
            IsDeleted = isDeleted;
            Createdby = createdby;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public string MeetingRoomId { get; set; }
        public string MeetingRoomName { get; set; }
        public string MeetingRoomMaxSize { get; set; }
        public bool HaveProjector { get; set; }
        public bool HaveTv { get; set; }
        public bool IsDeleted { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
