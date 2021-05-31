using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessModels
{
    public class UserModel
    {
        public UserModel(string userId, string loginName, string name, string email, int? phNumber, bool? isDeleted, string createdby, DateTime? createdDate, DateTime? modifedDate)
        {
            UserId = userId;
            LoginName = loginName;
            Name = name;
            Email = email;
            PhNumber = phNumber;
            IsDeleted = isDeleted;
            Createdby = createdby;
            CreatedDate = createdDate;
            ModifedDate = modifedDate;
        }
        public UserModel() : this("", "", "", "", null, false, "", DateTime.Now, DateTime.Now) { }
        public string UserId { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? PhNumber { get; set; }
        public bool? IsDeleted { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifedDate { get; set; }
    }
}
