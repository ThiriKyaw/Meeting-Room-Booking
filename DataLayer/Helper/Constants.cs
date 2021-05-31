using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingRoomBookingAPI.BusinessLogic.Helper
{
    public class Constants
    {

        public const string bookingOverLapError = "Meeting is overlapping. Please choose another time";
        public const string bookingSuccess = "Meeting Room is booked successfully";
        public const string bookingupdateSuccess = "Meeting Room Booking is updated successfully";
        public const string bookingUpdateError = "Update Meeeting Room Booking is failed. Please Try again";
        public const string bookingDeleteSuccess = "Meeting Room Booking is cancelled successfully";
        public const string bookingDeleteError = "Cancel Meeting Room Booking is failed. Please Try again";
        public const string Error = "The Process have been failed. Please try again later";
        public const string BookingTimeError = "You can't book the previsous Day. Please try again!";
        public const string BookingEditError = "You can't update the past bookings.";
    }
}
