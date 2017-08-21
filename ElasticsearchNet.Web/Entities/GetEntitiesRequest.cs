using System;
using System.Threading.Tasks;
using MediatR;

namespace ElasticsearchNet.Web.Entities
{
    public class GetEntitiesRequest : BaseEntityQuery
    {
        public GetEntitiesRequest(string index, string type) : base(index, type)
        {
        }
    }

    public class GetEntitiesRequestHandler : IAsyncRequestHandler<GetEntitiesRequest, IEntityResponse>
    {
        private readonly IDatabase database;

        public GetEntitiesRequestHandler(IDatabase database)
        {
            this.database = database;
        }

        public Task<IEntityResponse> Handle(GetEntitiesRequest message)
        {
            var result = this.database.GetDocuments(message.Index, message.Type);

            return Task.FromResult<IEntityResponse>(new EntityResponse(result));
        }
    }
}
