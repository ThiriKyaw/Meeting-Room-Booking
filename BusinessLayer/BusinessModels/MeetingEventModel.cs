using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessModels
{
    public class MeetingEventModel
    {
        public MeetingEventModel(string eventId, string eventName, string eventDescription, string meetingRoomId, string meetingRoomName, DateTime eventStartDateTime, DateTime eventEndDateTime, string eventCreatedby, string eventCreatedbyName, int? totalPeopleCount, bool isDeleted, DateTime? createdDate, DateTime? modifiedDate)
        {
            EventId = eventId;
            EventName = eventName;
            EventDescription = eventDescription;
            MeetingRoomId = meetingRoomId;
            MeetingRoomName = meetingRoomName;
            EventStartDateTime = eventStartDateTime;
            EventEndDateTime = eventEndDateTime;
            EventCreatedby = eventCreatedby;
            EventCreatedbyName = eventCreatedbyName;
            TotalPeopleCount = totalPeopleCount;
            IsDeleted = isDeleted;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public MeetingEventModel() : this("", "", "", "", "", DateTime.Now, DateTime.Now, "", "", 0, true, DateTime.Now, DateTime.Now) { }
        public string EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string MeetingRoomId { get; set; }
        public string MeetingRoomName { get; set; }
        public DateTime EventStartDateTime { get; set; }
        public DateTime EventEndDateTime { get; set; }
        public string EventCreatedby { get; set; }
        public string EventCreatedbyName { get; set; }
        public int? TotalPeopleCount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
