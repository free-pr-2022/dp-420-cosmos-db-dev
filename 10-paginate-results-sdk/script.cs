using System;
using Microsoft.Azure.Cosmos;

string endpoint = "https://tst-pr-20220614-11.documents.azure.com:443/";
string key = "7XriHHLIHp54b1BZ93dZW4oaBQE1aVvieb2ELIBdWftXBDS7V60CGZSNUDAWCre262JvCDoApk4Dv0gL9dR7Ow==";

CosmosClient client = new CosmosClient(endpoint, key);

Database database = await client.CreateDatabaseIfNotExistsAsync("cosmicworks");

Container container = await database.CreateContainerIfNotExistsAsync("products", "/categoryId");

string sql = "SELECT p.id, p.name, p.price FROM products p ";
QueryDefinition query = new (sql);

QueryRequestOptions options = new ();
options.MaxItemCount = 50;

FeedIterator<Product> iterator = container.GetItemQueryIterator<Product>(query, requestOptions: options);
while (iterator.HasMoreResults)
{    
    FeedResponse<Product> products = await iterator.ReadNextAsync(); 
    foreach (Product product in products)    
    {        
        Console.WriteLine($"[{product.id}]\t[{product.name,40}]\t[{product.price,10}]");    
    }
}