using BusinessLayer.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IMeetingEvent
    {
        public List<MeetingEventModel> CheckOverLap(MeetingEventModel meetingEventMdl);
        public List<MeetingEventModel> GetAllMeetingEvent();
        public String UpdateMeetingEvent(MeetingEventModel meetingEventMdl);
        public MeetingEventModel CreateMeetingEvent(MeetingEventModel meetingEventMdl);
        public String DeleteMeetingEvent(MeetingEventModel meetingEventMdl);

        public List<MeetingEventModel> GetMeetingListByRoomId(string RoomId);

    }
}
