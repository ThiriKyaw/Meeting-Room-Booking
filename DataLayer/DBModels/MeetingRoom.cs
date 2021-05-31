using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.DBModels
{
    public partial class MeetingRoom
    {
        public MeetingRoom()
        {
            MeetingEvents = new HashSet<MeetingEvent>();
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

        public virtual ICollection<MeetingEvent> MeetingEvents { get; set; }
    }
}
