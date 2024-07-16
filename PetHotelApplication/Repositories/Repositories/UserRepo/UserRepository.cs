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

        public List<User> GetUsers(string SearchValue, string sortOrder)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                var users = _context.Users.Include(x => x.Role)
                                          .Where(x => x.Role.RoleName != "Admin")
                                          .AsQueryable();

                if (!string.IsNullOrEmpty(SearchValue))
                {
                    users = users.Where(x => x.FullName.Contains(SearchValue) ||
                                             x.PhoneNumber.Contains(SearchValue) ||
                                             x.Email.Contains(SearchValue) ||
                                             x.Address.Contains(SearchValue) ||
                                             x.Status.Contains(SearchValue) ||
                                             x.Role.RoleName.Contains(SearchValue));
                }

                users = sortOrder switch
                {
                    "name_asc" => users.OrderBy(x => x.FullName),
                    "name_desc" => users.OrderByDescending(x => x.FullName),
                    "phone_asc" => users.OrderBy(x => x.PhoneNumber),
                    "phone_desc" => users.OrderByDescending(x => x.PhoneNumber),
                    "email_asc" => users.OrderBy(x => x.Email),
                    "email_desc" => users.OrderByDescending(x => x.Email),
                    "address_asc" => users.OrderBy(x => x.Address),
                    "address_desc" => users.OrderByDescending(x => x.Address),
                    "status_asc" => users.OrderBy(x => x.Status),
                    "status_desc" => users.OrderByDescending(x => x.Status),
                    "role_asc" => users.OrderBy(x => x.Role.RoleName),
                    "role_desc" => users.OrderByDescending(x => x.Role.RoleName),
                    _ => users.OrderBy(x => x.FullName),
                };

                return users.ToList();
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

        public bool isEmailExist(string email)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Users.Any(x => x.Email.Equals(email));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public bool isPhoneNumberExist(string phoneNumber)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Users.Any(x => x.PhoneNumber.Equals(phoneNumber));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public User GetUserById(string id)
        {
            try
            {
                using var _context = new PetHotelApplicationDbContext();
                return _context.Users.FirstOrDefault(x => x.Id.Equals(id.ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
