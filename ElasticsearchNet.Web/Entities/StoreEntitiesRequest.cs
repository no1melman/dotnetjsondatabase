using System;
using System.Threading.Tasks;
using ElasticsearchNet.Web.Controllers;
using MediatR;
using Newtonsoft.Json.Linq;

namespace ElasticsearchNet.Web.Entities
{
    public class StoreEntitiesRequest : BaseEntityQuery
    {
        public StoreEntitiesRequest(string index, string type, JObject document) : base(index, type)
        {
            Document = document;
        }

        public JObject Document { get; }
    }

    public class StoreEntitiesRequestHandler : IAsyncRequestHandler<StoreEntitiesRequest, IEntityResponse>
    {
        private readonly IDatabase database;

        public StoreEntitiesRequestHandler(IDatabase database)
        {
            this.database = database;
        }

        public Task<IEntityResponse> Handle(StoreEntitiesRequest message)
        {
            var result = this.database.StoreDocument(message.Index, message.Type, new Document(message.Document));

            return Task.FromResult<IEntityResponse>(new EntityResponse(result));
        }
    }
}
