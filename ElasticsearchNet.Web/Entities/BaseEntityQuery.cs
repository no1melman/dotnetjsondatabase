using System;
using MediatR;

namespace ElasticsearchNet.Web.Entities
{
    public abstract class BaseEntityQuery : IRequest<IEntityResponse>
    {
        public BaseEntityQuery(string index, string type)
        {
            Type = type;
            Index = index;
        }

		public string Index { get; }
		public string Type { get; }
    }
}
