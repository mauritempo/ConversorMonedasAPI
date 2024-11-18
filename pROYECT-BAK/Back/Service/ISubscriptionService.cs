using Data.entidades;
using DTO.SUBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ISubscriptionService
    {
        SubscriptionDTO GetSubscriptionByName(string name);
        UserSubStatusDTO AssignSubscriptionToUser(UserSubscriptionDTO userSubscriptionDto);
        bool HasConversionsLeft(int userId);
        int GetRemainingConversions(int userId);
        
    }
}
