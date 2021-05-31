using BusinessLayer.BusinessModels;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface IMeetingRoom
    {
        public List<MeetingRoomModel> GetAllMeetingRooms();
        public MeetingRoomModel GetMeetingRoombyMeetingRoomId(string RoomId);
    }
}
