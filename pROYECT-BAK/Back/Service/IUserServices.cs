using Data.entidades;
using DTO.USER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserServices
    {
        User CreateUser(UserRegistrationDTO registrationDto);
        List<User> GetUsers();
        User ValidateUser(string username, string password);
        User GetUserByUsername(string username);
    }
}