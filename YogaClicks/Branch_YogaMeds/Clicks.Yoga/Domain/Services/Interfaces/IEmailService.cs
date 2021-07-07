using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Services
{
    public interface IEmailService
    {
        void SendWelcomeEmail(User user);
    }
}
