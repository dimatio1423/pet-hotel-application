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
        private readonly PetHotelApplicationDbContext _context;
        private static UserRepository instance = null;
        private static readonly object padlock = new object();

        private UserRepository()
        {
            _context = new PetHotelApplicationDbContext(new DbContextOptions<PetHotelApplicationDbContext>());
        }

        public static UserRepository Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserRepository();
                    }
                    return instance;
                }
            }
        }

        public void Add(User user)
        {
            try
            {
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
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<User> GetListUsers()
        {
            try
            {
                return _context.Users.AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
