namespace ElasticsearchNet.Web.Entities
{
    public class RemoveEntity : BaseEntityQuery
    {
        public RemoveEntity(string index, string type, string id) : base(index, type)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
