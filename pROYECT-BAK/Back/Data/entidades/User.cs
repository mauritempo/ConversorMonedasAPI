using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data.entidades
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        // Clave foránea a la entidad Subscription
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }  // Relación de usuario con suscripción
        public ICollection<CurrencyConversion> Conversions { get; set; }
        
    }


}

    

