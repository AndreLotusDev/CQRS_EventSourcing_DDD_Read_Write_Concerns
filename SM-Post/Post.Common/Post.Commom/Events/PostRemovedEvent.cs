using CQRS.Core.Events;

namespace Post.Commom.Events
{
    public class PostRemovedEvent : BaseEvent
    {
        public PostRemovedEvent() : base(nameof(PostRemovedEvent))
        {
        }
    }
}
