using System;
using System.Linq;

using Microsoft.Azure.Cosmos;

string endpoint = "<cosmos-endpoint>";
string key = "<cosmos-key>";

CosmosClient client = new (endpoint, key);

AccountProperties account = await client.ReadAccountAsync();

Console.WriteLine($"Account Name:\t{account.Id}");
Console.WriteLine($"Primary Region:\t{account.WritableRegions.FirstOrDefault()?.Name}");
