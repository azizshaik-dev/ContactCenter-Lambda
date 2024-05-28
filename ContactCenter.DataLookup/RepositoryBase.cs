using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;


namespace ContactCenter.DataLookup
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly string _tableName;
        private readonly AmazonDynamoDBClient _client;
        private readonly Table _table;


        public RepositoryBase(string tableName, AmazonDynamoDBClient client)
        {
            _tableName = tableName;
            _client = client;
            _table = Table.LoadTable(_client, _tableName);
        }
        public async Task<T> CreateAsync(Document document)
        {
            await _table.PutItemAsync(document);
            return ConvertDocumentToEntity(document);
        }

        public async Task<T?> ReadAsync(string primaryKey, string sortKey = null)
        {
            var doc = await _table.GetItemAsync(primaryKey, sortKey);
            return ConvertDocumentToEntity(doc);
        }

        public async Task<T?> UpdateAsync(Document document)
        {
            await _table.UpdateItemAsync(document);
            return ConvertDocumentToEntity(document);
        }

        public async Task DeleteAsync(string primaryKey, string sortKey = null)
        {
            await _table.DeleteItemAsync(primaryKey, sortKey);
        }

        protected abstract T? ConvertDocumentToEntity(Document document);
        protected abstract Document? ConvertEntityToDocument(T entity);
    }
}
