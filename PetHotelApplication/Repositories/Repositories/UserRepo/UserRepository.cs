using BusinessObjects.Entities;
using BusinessObjects.Enums.StatusEnums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositories.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        public void Add(User user)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(User user)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                var currUser = _context.Users.FirstOrDefault(x => x.Id.Equals(user.Id));
                if (currUser != null)
                {
                    currUser.Status = StatusEnums.Inactive.ToString();
                    _context.Update(currUser);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("User not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Users.FirstOrDefault(x => x.Email.Equals(email));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Users.Where(x => x.RoleId != "1").Include(x => x.Role).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(User user)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }       
    }
}
