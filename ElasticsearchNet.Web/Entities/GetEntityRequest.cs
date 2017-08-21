using System;
using System.Threading.Tasks;
using MediatR;

namespace ElasticsearchNet.Web.Entities
{
    public class GetEntityRequest : BaseEntityQuery
    {
        public GetEntityRequest(string index, string type, string id) : base(index, type)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class GetEntityRequestHandler : IAsyncRequestHandler<GetEntityRequest, IEntityResponse>
    {
        readonly IDatabase database;

        public GetEntityRequestHandler(
            IDatabase database)
        {
            this.database = database;
        }

        public Task<IEntityResponse> Handle(GetEntityRequest message)
        {
            var result = this.database.GetDocument(message.Index, message.Type, message.Id);

            return Task.FromResult<IEntityResponse>(new EntityResponse(result));
        }
    }
}
