using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS.Core.Events;

namespace Post.Commom.Events
{
    public class MessageUpdateEvent : BaseEvent
    {
        public MessageUpdateEvent() : base(nameof(MessageUpdateEvent))
        {

        }

        public string Message { get; set; }
    }
}
