﻿using BusinessObjects.Entities;
using BusinessObjects.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.UserService
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUserByEmail(string email);
        void RegisterUser(RegisterUserReqModel registerUserReq);
        void CreateUserReq(CreateUserReqModel createUserReqModel);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        IQueryable<BusinessObjects.Entities.User> GetListUsers();
    }
}
