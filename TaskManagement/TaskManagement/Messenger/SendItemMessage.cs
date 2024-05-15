using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Messenger
{
    public class SendItemMessage<T> : ValueChangedMessage<T>
    {
        public SendItemMessage(T value) : base(value)
        {
        }
    }

}
