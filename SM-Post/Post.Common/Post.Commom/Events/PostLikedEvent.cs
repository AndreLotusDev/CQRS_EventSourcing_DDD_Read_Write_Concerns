﻿using CQRS.Core.Events;

namespace Post.Commom.Events
{
    public class PostLikedEvent : BaseEvent
    {
        public PostLikedEvent() : base(nameof(PostLikedEvent))
        {
        }
    }
}
