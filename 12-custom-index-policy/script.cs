using System;
using Microsoft.Azure.Cosmos;

string endpoint = "https://vvv-ooo.documents.azure.com:443/";

string key = "jm0Qj3sTtK0wGl1F2aFKlpIOxanxZUyMcKRKBOuxT9c42xN0PUkoc5MltC3y9KPQE6nWEWJT6M8EYghDBQCgtQ==";

CosmosClient client = new CosmosClient(endpoint, key);

Database database = await client.CreateDatabaseIfNotExistsAsync("cosmicworks");


IndexingPolicy policy = new ();

policy.IndexingMode = IndexingMode.Consistent;

policy.ExcludedPaths.Add(
    new ExcludedPath{ Path = "/*" }
);

policy.IncludedPaths.Add(
    new IncludedPath{ Path = "/name/?" }
);

ContainerProperties options = new ("products", "/categoryId");
options.IndexingPolicy = policy;

Container container = await database.CreateContainerIfNotExistsAsync(options);
Console.WriteLine($"Container Created [{container.Id}]");




