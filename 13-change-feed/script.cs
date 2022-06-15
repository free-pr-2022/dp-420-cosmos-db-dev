using Microsoft.Azure.Cosmos;
using static Microsoft.Azure.Cosmos.Container;

string endpoint = "https://sss113.documents.azure.com:443/";
string key = "kzoDF1xUmp0Q8VjBxO5lj5Bb4j4HDzFwlc4k8IDByG5zYUSPgmD4WmW92KsyTq2Jinma9qyCiC2UVWInQ48Lfw==";

CosmosClient client = new CosmosClient(endpoint, key);

Container sourceContainer = client.GetContainer("cosmicworks", "products");

Container leaseContainer = client.GetContainer("cosmicworks", "productslease");



ChangesHandler<Product> handleChanges = async (
    IReadOnlyCollection<Product> changes, 
    CancellationToken cancellationToken
) => {
    Console.WriteLine($"START\tHandling batch of changes...");
    foreach(Product product in changes)
    {
        await Console.Out.WriteLineAsync($"Detected Operation:\t[{product.id}]\t{product.name}");
    }
};

var builder = sourceContainer.GetChangeFeedProcessorBuilder<Product>(
    processorName: "productsProcessor",
    onChangesDelegate: handleChanges
);

ChangeFeedProcessor processor = builder
    .WithInstanceName("consoleApp")
    .WithLeaseContainer(leaseContainer)
    .Build();


await processor.StartAsync();

Console.WriteLine($"RUN\tListening for changes...");
Console.WriteLine("Press any key to stop");
Console.ReadKey(); 

await processor.StopAsync();

