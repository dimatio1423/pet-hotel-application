﻿using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.UserRepo
{
    public interface IUserRepository
    {
        List<User> GetUsers(string SearchValue, string sortOrder);
        User GetUserByEmail(string email);
        void Add(User user);
        void Delete(User user);
        void Update(User user);
    }
}
