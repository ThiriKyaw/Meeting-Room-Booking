using BusinessLayer.BusinessModels;
using DataLayer.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.BusinessLogic.Helper
{
    public class ConvertToBusinessModel
    {
        public static UserModel ConvertDbMdlToBussinesMdl(User user)  => new UserModel(user.UserId, user.LoginName, user.Name, user.Email,user.PhNumber, user.IsDeleted, user.Createdby, user.CreatedDate, user.ModifedDate);
        public static MeetingRoomModel ConvertDbMdlToBussinesMdl(MeetingRoom mb)  => new MeetingRoomModel(mb.MeetingRoomId,mb.MeetingRoomName,mb.MeetingRoomMaxSize, mb.HaveProjector, mb.HaveTv, mb.IsDeleted, mb.Createdby, mb.CreatedDate, mb.ModifiedDate);
        public static MeetingEventModel ConvertDbMdlToBussinesMdl(MeetingRoom room, User user, MeetingEvent mEvent) => new MeetingEventModel(mEvent.EventId, mEvent.EventName, mEvent.EventDescription, mEvent.MeetingRoomId, room.MeetingRoomName, mEvent.EventStartDateTime, mEvent.EventEndDateTime, mEvent.EventCreatedby,user.Name, mEvent.TotalPeopleCount, mEvent.IsDeleted, mEvent.CreatedDate, mEvent.ModifiedDate);
    }
}
