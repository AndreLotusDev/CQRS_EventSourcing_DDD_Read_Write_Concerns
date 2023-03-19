using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS.Core.Infraestructure;
using CQRS.Core.Queries;
using Post.Cmd.Domain.Entities;

namespace Post.Query.Infrastructure.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher<PostEntity>
    {
        private readonly Dictionary<Type, Func<BaseQuery, Task<List<PostEntity>>>> _handlers = new();

        public void RegisterHandler<TQuery>(Func<TQuery, Task<List<PostEntity>>> handler) where TQuery : BaseQuery
        {
            if (_handlers.ContainsKey(typeof(TQuery)))
            {
                throw new ArgumentException($"Handler for {typeof(TQuery)} is already registered");
            }

            _handlers.Add(typeof(TQuery), query => handler((TQuery)query));
        }

        public async Task<List<PostEntity>> SendAsync(BaseQuery query)
        {
            if (!_handlers.ContainsKey(query.GetType()))
            {
                throw new ArgumentException($"Handler for {query.GetType()} is not registered");
            }

            return await _handlers[query.GetType()](query);
        }
    }
}
