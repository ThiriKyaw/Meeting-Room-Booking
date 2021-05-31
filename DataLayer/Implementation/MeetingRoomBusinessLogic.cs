using BusinessLayer.BusinessModels;
using DataLayer.DBModels;
using System.Collections.Generic;
using DataLayer.BusinessLogic.Helper;
using BusinessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataLayer.Implementation
{
    public class MeetingRoomBusinessLogic : IMeetingRoom
    {
        private readonly MeetingRoomBookingContext _context;
        public MeetingRoomBusinessLogic(MeetingRoomBookingContext context)
        {
            _context = context;
        }
        public List<MeetingRoomModel> GetAllMeetingRooms()
        {
            List<MeetingRoom> meetingRoomList = _context.MeetingRooms.AsNoTracking().Where(x => x.IsDeleted == false).ToList();
            List<MeetingRoomModel> meetingRoomModelList = new List<MeetingRoomModel>();
            foreach (var meetingRoom in meetingRoomList)
            {
                meetingRoomModelList.Add(ConvertToBusinessModel.ConvertDbMdlToBussinesMdl(meetingRoom));
            }
            return meetingRoomModelList;
        }

        public MeetingRoomModel GetMeetingRoombyMeetingRoomId(string meetingRoomId)
        {
            MeetingRoom meetingRoom = _context.MeetingRooms.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.MeetingRoomId == meetingRoomId);
            MeetingRoomModel objMeetingRoomMdl = ConvertToBusinessModel.ConvertDbMdlToBussinesMdl(meetingRoom);
            return objMeetingRoomMdl;
        }
    }
}
