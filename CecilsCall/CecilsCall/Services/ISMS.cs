using System;
using System.Collections.Generic;
using System.Text;

namespace CecilsCall.Services
{
    public interface ISMS
    {
        void SendSMS(string phoneNumber, string msg);
        void SendSMStoAssociates(string ownersName);
    }
}
