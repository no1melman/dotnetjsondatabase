using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ElasticsearchNet.Web
{
    public interface IDatabase
    {
        bool StoreDocument(string index, string type, IDocument document);

        bool RemoveDocument(string index, string type, string id);

        IEnumerable<IDocument> GetDocuments(string index, string type);

        IDocument GetDocument(string index, string type, string id);
    }

    public class MemoryDatabase : IDatabase
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, IList<IDocument>>> database;

        private static int CurrentId;

        public MemoryDatabase()
        {
            database = new ConcurrentDictionary<string, ConcurrentDictionary<string, IList<IDocument>>>();
        }

        public bool StoreDocument(string index, string type, IDocument document)
        {
            var typeDatabase = database.GetOrAdd(index, (arg) => new ConcurrentDictionary<string, IList<IDocument>>());

            var documents = typeDatabase.GetOrAdd(type, (arg) => new List<IDocument>());

            IDocument foundDocument = documents.FirstOrDefault();
            if (foundDocument == null) {
                if (document.Content["Id"].ToString() == null) {
                    document.Content["Id"] = NewId().ToString();
                }

                documents.Add(document);

                return true;
            }

            documents.Remove(foundDocument);
            documents.Add(document);

            return true;
        }

        public bool RemoveDocument(string index, string type, string id) 
        {
            database.TryGetValue(index, out var typeDatabse);

            if (typeDatabse == null) 
            {
                return true;
            }

            typeDatabse.TryGetValue(type, out var documents);

            var foundDocument = documents.FirstOrDefault(x => x.Content["Id"].ToString() == id);

            if (foundDocument == null) 
            {
                return true;
            }

            documents.Remove(foundDocument);

            return true;
        }

        public IEnumerable<IDocument> GetDocuments(string index, string type)
        {
            database.TryGetValue(index, out var typeDatabase);

            if (typeDatabase == null) 
            {
                return Enumerable.Empty<IDocument>();
            }

            typeDatabase.TryGetValue(type, out var documents);

            return documents ?? Enumerable.Empty<IDocument>();
        }

        public IDocument GetDocument(string index, string type, string id)
        {
            var documents = this.GetDocuments(index, type);

            return documents.FirstOrDefault(x => x.Content["Id"].ToString() == id);
        }

		private int NewId()
		{
			return CurrentId++;
		}
    }

    public interface IDocument
    {
        JObject Content { get; }   
    }
}
