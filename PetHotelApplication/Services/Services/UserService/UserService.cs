using BusinessObjects.Entities;
using BusinessObjects.Enums.RoleEnums;
using BusinessObjects.Enums.StatusEnums;
using BusinessObjects.Models.UserModel;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void CreateUserReq(CreateUserReqModel createUserReqModel)
        {
            User newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Avatar = createUserReqModel.Avatar ?? "link",
                FullName = createUserReqModel.FullName,
                PhoneNumber = createUserReqModel.PhoneNumber,
                Email = createUserReqModel.Email,
                Password = HashPassword(createUserReqModel.Password),
                Address = createUserReqModel.Address,
                Status = StatusEnums.Active.ToString(),
                RoleId = createUserReqModel.RoleId
            };

            _userRepository.Add(newUser);
        }

        public void RegisterUser(RegisterUserReqModel registerUserReq)
        {
            User newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Avatar = "",
                FullName = registerUserReq.FullName,
                PhoneNumber = registerUserReq.PhoneNumber,
                Email = registerUserReq.Email,
                Password = HashPassword(registerUserReq.Password),
                Address = registerUserReq.Address,
                Status = StatusEnums.Active.ToString(),
                RoleId = (((int)RoleEnums.Customer) + 1).ToString(),
            };

            _userRepository.Add(newUser);

        }


        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash value of the password bytes
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash bytes to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                // Return the hashed password as a string
                return sb.ToString();
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            string hashedInput = HashPassword(password);
            return string.Equals(hashedInput, hashedPassword, StringComparison.OrdinalIgnoreCase);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public bool isEmailExist(string email)
        {
            return _userRepository.isEmailExist(email);
        }

        public bool isPhoneNumberExist(string phoneNumber)
        {
            return _userRepository.isPhoneNumberExist(phoneNumber);
        }

        public User GetUserById(string id)
        {
            return _userRepository.GetUserById(id);
        }
        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}
