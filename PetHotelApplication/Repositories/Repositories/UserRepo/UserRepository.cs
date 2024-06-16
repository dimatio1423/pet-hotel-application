using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var currUser = _context.Pets.FirstOrDefault(x => x.Id.Equals(user.Id));

                currUser.Status = "Inactive";

                _context.Update(currUser);

                _context.SaveChanges();
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
                throw new Exception(ex.Message);
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Users.ToList();
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
                //_context.Categories.Update(category);
                _context.Entry<User>(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
