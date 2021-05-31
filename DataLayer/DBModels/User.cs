using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.DBModels
{
    public partial class User
    {
        public User()
        {
            MeetingEvents = new HashSet<MeetingEvent>();
        }

        public string UserId { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? PhNumber { get; set; }
        public bool? IsDeleted { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifedDate { get; set; }

        public virtual ICollection<MeetingEvent> MeetingEvents { get; set; }
    }
}
