using BusinessLayer.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUser
    {
        public List<UserModel> GetAllUser();
    }
}
