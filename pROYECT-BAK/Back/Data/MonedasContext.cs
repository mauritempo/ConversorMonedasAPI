using Data.entidades;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

    public class MonedasContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<CurrencyConversion> currencyConversions { get; set; }


        public MonedasContext(DbContextOptions<MonedasContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la relación entre User y Subscription
            modelBuilder.Entity<User>()
                .HasOne(u => u.Subscription)
                .WithMany()
                .HasForeignKey(u => u.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subscription>().HasData(
            new Subscription { Id = 1, Name = "Free", MaxConversions = 10 },
            new Subscription { Id = 2, Name = "Trial", MaxConversions = 100 },
            new Subscription { Id = 3, Name = "Pro", MaxConversions = int.MaxValue }
            );
            modelBuilder.Entity<User>().HasData(

                new User
                {
                    Id = 3,
                    Username = "mauri",
                    Password = "mauri", 
                    Email = "mauri@example.com",
                    IsActive = true,
                    SubscriptionId = 1
                },


                new User
                {
                    Id = 4,
                    Username = "mauri2",
                    Password = "mauri2", 
                    Email = "mauri@example2.com",
                    IsActive = true,
                    SubscriptionId = 2
                },
                new User
                {
                    Id = 5,
                    Username = "mauri3",
                    Password = "mauri3", 
                    Email = "mauri@example3.com",
                    IsActive = true,
                    SubscriptionId = 3
                }
                );
            modelBuilder.Entity<CurrencyConversion>().HasData(
                new CurrencyConversion
                {
                    Id = 1,
                    Code = "ARS",
                    Legend = "Peso Argentino",
                    Symbol = "$",
                    ConvertibilityIndex = 0.002m,
                    Status = CurrencyStatus.Alta // O el estado que consideres inicial
                },
                new CurrencyConversion
                {
                    Id = 2,
                    Code = "EUR",
                    Legend = "Euro",
                    Symbol = "€",
                    ConvertibilityIndex = 1.09m,
                    Status = CurrencyStatus.Alta
                },
                new CurrencyConversion
                {
                    Id = 3,
                    Code = "KC",
                    Legend = "Corona Checa",
                    Symbol = "Kč",
                    ConvertibilityIndex = 0.043m,
                    Status = CurrencyStatus.Alta
                },
                new CurrencyConversion
                {
                    Id = 4,
                    Code = "USD",
                    Legend = "Dólar Americano",
                    Symbol = "$",
                    ConvertibilityIndex = 1m,
                    Status = CurrencyStatus.Alta
                }
            );
        }
    }
}