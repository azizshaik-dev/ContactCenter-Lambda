using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using ContactCenter.DataLookup;
using ContactCenter.DataModels;

namespace ContactCenter.IntentLookup.cs
{
    internal class IntentRepo : RepositoryBase<IntentToQueueMap>
    {
        IntentRepo(AmazonDynamoDBClient client) : base("IntentMapTable", client)
        {
        }

        protected override IntentToQueueMap? ConvertDocumentToEntity(Document document)
        {
            if (document == null)
                return null;

            return new IntentToQueueMap
            {
                Intent = document["Intent"],
                Language = document["Language"],
                QueueArn = document["QueueArn"]
            };
        }

        protected override Document? ConvertEntityToDocument(IntentToQueueMap entity)
        {
            var document = new Document
            {
                ["Intent"] = entity.Intent,
                ["Language"] = entity.Language,
                ["QueueArn"] = entity.QueueArn
            };
            return document;
        }
    }
}
