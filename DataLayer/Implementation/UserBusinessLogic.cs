using BusinessLayer.BusinessModels;
using DataLayer.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using DataLayer.BusinessLogic.Helper;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Implementation
{
    public class UserBusinessLogic : IUser
    {
        private readonly MeetingRoomBookingContext _context;
        public UserBusinessLogic(MeetingRoomBookingContext context) {
            _context = context;
        }

        public List<UserModel> GetAllUser()
        {
            List<UserModel> userModelList = new List<UserModel>();
            List<User> allUserList = _context.Users.AsNoTracking().Where(x => x.IsDeleted == false).ToList();
            foreach (var user in allUserList)
            {
                userModelList.Add(ConvertToBusinessModel.ConvertDbMdlToBussinesMdl(user));
            }
            return userModelList;
        }
    }
}
