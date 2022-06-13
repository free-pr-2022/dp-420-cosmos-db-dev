using System;
using Microsoft.Azure.Cosmos;

string endpoint = "https://ts-pr-20220612v7.documents.azure.com:443/";
string key = "brwpWgzMR6pwtbwjj8UNpicyqbWppacrb4Z8Xf8JiCk9J9U38s6DlJRF0sYqJGyWvXnrVgy72uQqCxA6FrS6Vg==";

CosmosClient client = new CosmosClient(endpoint, key);
    
Database database = await client.CreateDatabaseIfNotExistsAsync("cosmicworks");
Container container = await database.CreateContainerIfNotExistsAsync("products", "/categoryId", 400);


Product saddle = new("0120", "Worn Saddle", "9603ca6c-9e28-4a02-9194-51cdb7fea816");
Product handlebar = new("012A", "Rusty Handlebar", "9603ca6c-9e28-4a02-9194-51cdb7fea816");

PartitionKey partitionKey = new ("9603ca6c-9e28-4a02-9194-51cdb7fea816");

TransactionalBatch batch = container.CreateTransactionalBatch(partitionKey)
    .CreateItem<Product>(saddle)
    .CreateItem<Product>(handlebar);

using TransactionalBatchResponse response = await batch.ExecuteAsync();

Console.WriteLine($"Status:\t{response.StatusCode}");

