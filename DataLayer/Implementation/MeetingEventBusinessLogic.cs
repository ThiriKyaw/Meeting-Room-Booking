using MeetingRoomBookingAPI.BusinessLogic.Helper;
using BusinessLayer.BusinessModels;
using DataLayer.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using DataLayer.BusinessLogic.Helper;

namespace DataLayer.Implementation
{
    public class MeetingEventBusinessLogic : IMeetingEvent
    {
        private readonly MeetingRoomBookingContext _context;
        public MeetingEventBusinessLogic(MeetingRoomBookingContext context)
        {
            _context = context;
        }
        public List<MeetingEventModel> CheckOverLap(MeetingEventModel meetingEventMdl)
        {
            List<MeetingEventModel> overlapMeetingList = new List<MeetingEventModel>();
            List<MeetingEvent> meetingEventList = _context.MeetingEvents.Where(x => x.MeetingRoomId == meetingEventMdl.MeetingRoomId && x.EventStartDateTime.Date == meetingEventMdl.EventStartDateTime.Date && x.IsDeleted == false).ToList();
            meetingEventList = meetingEventList.Where(x => !x.EventId.Contains(meetingEventMdl.EventId)).ToList();
            foreach (var meetingevent in meetingEventList)
            {
                if (meetingevent.EventStartDateTime.Date == meetingevent.EventEndDateTime.Date) { 
                if (meetingEventMdl.EventStartDateTime.TimeOfDay >= meetingevent.EventStartDateTime.TimeOfDay &&
                         meetingEventMdl.EventStartDateTime.TimeOfDay < meetingevent.EventEndDateTime.TimeOfDay)
                {
                        User user = _context.Users.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.UserId == meetingevent.EventCreatedby);
                        MeetingRoom meetingroom = _context.MeetingRooms.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.MeetingRoomId == meetingevent.MeetingRoomId);
                        overlapMeetingList.Add(ConvertToBusinessModel.ConvertDbMdlToBussinesMdl(meetingroom, user, meetingevent));
                }
                else if (meetingEventMdl.EventEndDateTime.TimeOfDay > meetingevent.EventStartDateTime.TimeOfDay &&
                    meetingEventMdl.EventEndDateTime.TimeOfDay <= meetingevent.EventEndDateTime.TimeOfDay)
                {
                        User user = _context.Users.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.UserId == meetingevent.EventCreatedby);
                        MeetingRoom meetingroom = _context.MeetingRooms.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.MeetingRoomId == meetingevent.MeetingRoomId);
                        overlapMeetingList.Add(ConvertToBusinessModel.ConvertDbMdlToBussinesMdl(meetingroom, user, meetingevent));
                }
                else if (meetingEventMdl.EventStartDateTime.TimeOfDay < meetingevent.EventStartDateTime.TimeOfDay &&
                    meetingEventMdl.EventEndDateTime.TimeOfDay > meetingevent.EventEndDateTime.TimeOfDay)
                {
                        User user = _context.Users.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.UserId == meetingevent.EventCreatedby);
                        MeetingRoom meetingroom = _context.MeetingRooms.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.MeetingRoomId == meetingevent.MeetingRoomId);
                        overlapMeetingList.Add(ConvertToBusinessModel.ConvertDbMdlToBussinesMdl(meetingroom, user, meetingevent));
                }
                }
            }
            return overlapMeetingList;
        }

        public List<MeetingEventModel> GetAllMeetingEvent()
        {
            List<MeetingEventModel> AllEventMdl = new List<MeetingEventModel>();
            List<MeetingEvent> AllEvent = _context.MeetingEvents.AsNoTracking().Where(x => x.IsDeleted == false).ToList();
            foreach (var mEvent in AllEvent)
            {
                User user = _context.Users.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.UserId == mEvent.EventCreatedby);
                MeetingRoom meetingroom = _context.MeetingRooms.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.MeetingRoomId == mEvent.MeetingRoomId);
                AllEventMdl.Add(ConvertToBusinessModel.ConvertDbMdlToBussinesMdl(meetingroom, user, mEvent));
            }
            return AllEventMdl;
        }

        public List<MeetingEventModel> GetMeetingListByRoomId(string RoomId)
        {
            List<MeetingEventModel> AllEventMdl = new List<MeetingEventModel>();
            List<MeetingEvent> AllEvent = _context.MeetingEvents.AsNoTracking().Where(x => x.IsDeleted == false && x.MeetingRoomId == RoomId).ToList();
            foreach (var mEvent in AllEvent)
            {
                User user = _context.Users.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.UserId == mEvent.EventCreatedby);
                MeetingRoom meetingroom = _context.MeetingRooms.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.MeetingRoomId == mEvent.MeetingRoomId);
                AllEventMdl.Add(ConvertToBusinessModel.ConvertDbMdlToBussinesMdl(meetingroom, user, mEvent));
            }
            return AllEventMdl;
        }


        public MeetingEventModel CreateMeetingEvent(MeetingEventModel meetingEventMdl) {
            if (meetingEventMdl.EventStartDateTime < DateTime.Now.AddMinutes(-5)) 
            {
                MeetingEventModel meetingEventObj = new MeetingEventModel();
                meetingEventObj.EventId = "1";
                return meetingEventObj;
            }
            List<MeetingEventModel> overLapMeeting = CheckOverLap(meetingEventMdl);
            if (overLapMeeting.Count == 0)
            {
                MeetingEvent meetingEvent = new MeetingEvent();
                meetingEvent.EventId = Guid.NewGuid().ToString();
                meetingEvent.EventName = meetingEventMdl.EventName;
                meetingEvent.EventDescription = meetingEventMdl.EventDescription;
                meetingEvent.EventStartDateTime = meetingEventMdl.EventStartDateTime;
                meetingEvent.EventEndDateTime = meetingEventMdl.EventEndDateTime;
                meetingEvent.MeetingRoomId = meetingEventMdl.MeetingRoomId;
                meetingEvent.EventCreatedby = meetingEventMdl.EventCreatedby;
                meetingEvent.TotalPeopleCount = meetingEventMdl.TotalPeopleCount;
                meetingEvent.IsDeleted = false;
                meetingEvent.CreatedDate = DateTime.Now;
                meetingEvent.ModifiedDate = DateTime.Now;
                _context.MeetingEvents.AddAsync(meetingEvent);
                _context.SaveChanges();
                User user = _context.Users.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.UserId == meetingEvent.EventCreatedby);
                MeetingRoom meetingroom = _context.MeetingRooms.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.MeetingRoomId == meetingEvent.MeetingRoomId);
                MeetingEventModel objEventMdl = ConvertToBusinessModel.ConvertDbMdlToBussinesMdl(meetingroom, user, meetingEvent);
                return objEventMdl;
            }
            else {
                return new MeetingEventModel() ;
            }
           
        }
        public String UpdateMeetingEvent(MeetingEventModel meetingEventMdl)
        {
            List<MeetingEventModel> overLapMeeting = CheckOverLap(meetingEventMdl);
            if (overLapMeeting.Count == 0)
            {
                MeetingEvent meetingEvent = _context.MeetingEvents.FirstOrDefault(x => x.EventId == meetingEventMdl.EventId);
                meetingEvent.EventName = meetingEventMdl.EventName;
                meetingEvent.EventDescription = meetingEventMdl.EventDescription;
                meetingEvent.EventStartDateTime = meetingEventMdl.EventStartDateTime;
                meetingEvent.EventEndDateTime = meetingEventMdl.EventEndDateTime;
                meetingEvent.MeetingRoomId = meetingEventMdl.MeetingRoomId;
                meetingEvent.TotalPeopleCount = meetingEventMdl.TotalPeopleCount;
                meetingEvent.ModifiedDate = DateTime.Now;
                _context.SaveChanges();
                return Constants.bookingupdateSuccess;
            }
            else
            {
                return Constants.bookingOverLapError;
            }

        }
        public String DeleteMeetingEvent(MeetingEventModel meetingEventMdl)
        {
            MeetingEvent meetingevent = _context.MeetingEvents.FirstOrDefault(x => x.EventId == meetingEventMdl.EventId);
            meetingevent.IsDeleted = true;
            _context.SaveChanges();
            return Constants.bookingDeleteSuccess;
        }
    }
}
