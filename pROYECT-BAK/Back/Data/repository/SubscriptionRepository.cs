using Data.entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.repository
{
    public class SubscriptionRepository
    {
        private readonly MonedasContext _context;
        public SubscriptionRepository(MonedasContext context)
        {
            _context = context;
        }

        public Subscription GetSubscriptionByName(string name)
        {
            return _context.Subscriptions
                .FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public User GetUserById(int userId)// obtener un usuario específico por su ID, incluyendo su suscripción
        {
            return _context.Users
                .Include(u => u.Subscription)  
                .FirstOrDefault(u => u.Id == userId);
        }

        public User AssignSubscriptionToUser(int userId, int subscriptionId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.SubscriptionId = subscriptionId;
                _context.SaveChanges();
                return user; // Retorna el usuario actualizado
            }
            return null; 
        }


        public bool HasConversionsLeft(int userId)// Método para verificar si un usuario tiene suficientes conversiones restantes
        {
            var user = _context.Users
                .Include(u => u.Subscription)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null || user.Subscription == null)
            {
                return false; 
            }

            int conversionsUsed = _context.currencyConversions.Count(c => c.UserId == userId);

           
            return user.Subscription.MaxConversions == int.MaxValue || conversionsUsed < user.Subscription.MaxConversions; // Verificar el límite de conversiones según el tipo de suscripción
        }

       
        public int GetRemainingConversions(int userId) // obtener el límite de conversiones de un usuario
        {
            var user = _context.Users
                .Include(u => u.Subscription)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null || user.Subscription == null)
            {
                return 0; 
            }

            int conversionsUsed = _context.currencyConversions.Count(c => c.UserId == userId);

            
            return (user.Subscription.MaxConversions == int.MaxValue) ? int.MaxValue : user.Subscription.MaxConversions - conversionsUsed;// Calcula y retorna el número restante de conversiones
        }
        

    }
}
