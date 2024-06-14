using Amazon.DynamoDBv2.DocumentModel;

namespace ContactCenter.DataLookup
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task<T> CreateAsync(Document document);
        public Task<T?> ReadAsync(string primaryKey, string sortKey = null);
        public Task<T?> UpdateAsync(Document document);
        public Task DeleteAsync(string primaryKey, string sortKey = null);
        
    }
}