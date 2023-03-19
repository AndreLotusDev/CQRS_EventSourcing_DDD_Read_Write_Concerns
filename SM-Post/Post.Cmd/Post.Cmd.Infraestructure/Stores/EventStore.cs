using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;
using CQRS.Core.Infraestructure;
using CQRS.Core.Producers;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Infrastructure.Stores;

public class EventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IEventProducer _eventProducer;

    public EventStore(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer)
    {
        _eventStoreRepository = eventStoreRepository;
        _eventProducer = eventProducer;
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);
        
        if(expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            throw new ConcurrencyException($"Aggregate {aggregateId} was modified by another user");

        var version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            @event.Version = version;
            var eventType = @events.GetType().Name;
            var eventModel = new EventModel()
            {
                TimeStamp = DateTime.Now,
                AggregateIdentifier = aggregateId,
                AggregateType = nameof(PostAggregate),
                Version = version,
                EventType = eventType,
                EventData = @event,
            };
            
            await _eventStoreRepository.SaveAsync(eventModel);
            
            await _eventProducer.ProduceAsync("SocialMediaPostEvents", @event);
        }
    }

    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
    {
        var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);
        
        if(eventStream == null || !eventStream.Any())
            throw new AggregateNotFoundException($"Aggregate {aggregateId} not found");

        return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }

    public async Task<List<Guid>> GetAggregateIdsAsync()
    {
        var eventStream = await _eventStoreRepository.FindAllAsync();

        if (eventStream == null || !eventStream.Any())
        {
            throw new Exception("Could not retrieve event stream from the event store!");
        }

        return eventStream.Select(x => x.AggregateIdentifier).Distinct().ToList();
    }
}