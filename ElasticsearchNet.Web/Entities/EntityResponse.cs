using System;
namespace ElasticsearchNet.Web.Entities
{
    public class EntityResponse : IEntityResponse
    {
        public EntityResponse(
            object result)
        {
            this.Result = result;
        }

        public object Result { get; }
    }

    public interface IEntityResponse
    {
        object Result { get; }
    }
}
