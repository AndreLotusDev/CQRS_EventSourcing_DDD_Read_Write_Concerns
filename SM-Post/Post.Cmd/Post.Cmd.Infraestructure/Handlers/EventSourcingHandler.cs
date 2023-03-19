using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infraestructure;
using CQRS.Core.Producers;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Infrastructure.Handlers;

public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
{
    private readonly IEventStore _eventStore;
    private readonly IEventProducer _eventProducer;
    
    public EventSourcingHandler(IEventStore eventStore, IEventProducer producer)
    {
        _eventStore = eventStore;
        _eventProducer = producer;
    }
    
    public async Task SaveAsync(AggregateRoot aggregateRoot)
    {
        await _eventStore.SaveEventsAsync(
            aggregateRoot.Id,
            aggregateRoot.GetUncommittedChanges(), 
            aggregateRoot.Version);
        
        aggregateRoot.MarkChangesAsCommitted();
    }

    public async Task<PostAggregate> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new PostAggregate();
        var events = await _eventStore.GetEventsAsync(aggregateId);
        
        if(events == null || !events.Any())
            return aggregate;
        
        aggregate.ReplayEvents(events);
        var latestVersion = events.Select(s => s.Version).Max();
        aggregate.Version = latestVersion;
        
        return aggregate;
    }

    public async Task RepublishEventsAsync()
    {
        var aggregatesIds = await _eventStore.GetAggregateIdsAsync();

        if (aggregatesIds == null || !aggregatesIds.Any()) return;

        foreach (var aggregateId in aggregatesIds)
        {
            var aggregate = await GetByIdAsync(aggregateId);

            if (aggregate == null || !aggregate.Active) continue;

            var events = await _eventStore.GetEventsAsync(aggregateId);

            foreach (var @event in events)
            {
                await _eventProducer.ProduceAsync("SocialMediaPostEvents", @event);
            }
        }
    }
}