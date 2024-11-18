using Data.entidades;
using Data.repository;
using DTO.SUBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly SubscriptionRepository _subscriptionRepository;
        private readonly CurrencyRepository _conversionRepository;

        public SubscriptionService(SubscriptionRepository subscriptionRepository, CurrencyRepository conversionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _conversionRepository = conversionRepository;
        }

        // Obtener la suscripción por nombre (Free, Trial, Pro)
        public SubscriptionDTO GetSubscriptionByName(string name)
        {
            var subscription = _subscriptionRepository.GetSubscriptionByName(name);
            return new SubscriptionDTO
            {
                Id = subscription.Id,
                Name = subscription.Name,
                MaxConversions = subscription.MaxConversions
            };
        }

        // Asignar una suscripción a un usuario
        public UserSubStatusDTO AssignSubscriptionToUser(UserSubscriptionDTO userSubscriptionDto)
        {
            var updatedUser = _subscriptionRepository.AssignSubscriptionToUser(userSubscriptionDto.UserId, userSubscriptionDto.SubscriptionId);

            if (updatedUser == null)
            {
                return null; // Retornar null si no se encontró el usuario o la suscripción
            }

            // Obtener las conversiones usadas para calcular las restantes
            int conversionsUsed = _subscriptionRepository.GetRemainingConversions(updatedUser.Id);

            return new UserSubStatusDTO
            {
                UserId = updatedUser.Id,
                SubscriptionName = updatedUser.Subscription.Name,
                MaxConversions = updatedUser.Subscription.MaxConversions,
                ConversionsUsed = conversionsUsed,
                ConversionsRemaining = updatedUser.Subscription.MaxConversions == int.MaxValue
                                       ? int.MaxValue
                                       : updatedUser.Subscription.MaxConversions - conversionsUsed
            };
        }


        public bool HasConversionsLeft(int userId)// Verificar si el usuario tiene conversiones restantes
        {
            var user = _subscriptionRepository.GetUserById(userId);

            if (user == null || user.Subscription == null)
            {
                return false;
            }

            int conversionsUsed = _conversionRepository.GetConversionsCountByUserId(userId);

            return user.Subscription.MaxConversions == int.MaxValue || conversionsUsed < user.Subscription.MaxConversions;
        }


        public int GetRemainingConversions(int userId)// Obtener el número de conversiones restantes de un usuario
        {
            var user = _subscriptionRepository.GetUserById(userId);

            if (user == null || user.Subscription == null)
            {
                return 0;
            }

            int conversionsUsed = _conversionRepository.GetConversionsCountByUserId(userId);


            return (user.Subscription.MaxConversions == int.MaxValue) ? int.MaxValue : user.Subscription.MaxConversions - conversionsUsed;
        }
    }   }
