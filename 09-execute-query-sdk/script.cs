using System;
using Azure.Cosmos;

string endpoint = "https://cosmos24040016.documents.azure.com:443/";

string key = "z5nnAI8E6grFdra8LsvApX82oJX5N4Q07vbzinfnbFf3BgZZZgpHmpDYUi2rthDZrb0SxFD5aWJkPTyynB7FGA==";

CosmosClient client = new CosmosClient(endpoint, key);

CosmosDatabase database = await client.CreateDatabaseIfNotExistsAsync("cosmicworks");

CosmosContainer container = await database.CreateContainerIfNotExistsAsync("products", "/categoryId");

// cosmicworks --endpoint "https://cosmos24040016.documents.azure.com:443/" --key "z5nnAI8E6grFdra8LsvApX82oJX5N4Q07vbzinfnbFf3BgZZZgpHmpDYUi2rthDZrb0SxFD5aWJkPTyynB7FGA==" --datasets product
string sql = "SELECT * FROM products p";
QueryDefinition query = new (sql);

await foreach (Product product in container.GetItemQueryIterator<Product>(query))
{
    Console.WriteLine($"[{product.id}]\t{product.name,35}\t{product.price,15:C}");
}