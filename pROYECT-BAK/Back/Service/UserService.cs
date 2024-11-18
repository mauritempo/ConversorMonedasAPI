using Data.entidades;
using Data.repository;
using DTO.USER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
    public class UserServices: IUserServices
    {
        private readonly UserRepository _userRepository;

        public UserServices(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User CreateUser(UserRegistrationDTO registrationDto )
        {
            // Crear un nuevo usuario
            var user = new User
            {
                Username = registrationDto.Username,
                Password = registrationDto.Password,
                Email = registrationDto.Email,
                SubscriptionId = registrationDto.SubscriptionId,
                IsActive = true
            };

            _userRepository.AddUser(user);
            return user;
        }


        public List<User> GetUsers()
        {
           
            return _userRepository.GetUsers();
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }


        public User ValidateUser(string username, string password)
        {
            // Buscar el usuario por nombre de usuario
            var user = _userRepository.GetUserByUsername(username);

            // Verificar que el usuario existe y la contraseña es correcta
            if (user != null && user.Password == password)
            {
                return user; // Devolver el usuario si las credenciales son válidas
            }

            return null; // Devolver null si las credenciales no son válidas
        }


    }
}
