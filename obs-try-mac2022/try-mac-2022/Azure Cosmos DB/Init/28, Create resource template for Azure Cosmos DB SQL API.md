
# Create resource template for Azure Cosmos DB SQL API

## Learning objectives

-   Identify the three most common resource types for Azure Cosmos DB SQL API accounts
-   Create and deploy an JSON Azure Resource Manager template for an Azure Cosmos DB SQL API account, database, or container
-   Create and deploy a Bicep template for an Azure Cosmos DB SQL API account, database, or container
-   Manage throughput and index policies using JSON or Bicep templates

Template sample:
```json
{
  "$schema": "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#",
  "contentVersion": "1.0.0.0",
  "resources": [
    {
      "type": "Microsoft.DocumentDB/databaseAccounts",
      "apiVersion": "2021-05-15",
      "name": "[concat('csmsarm', uniqueString(resourceGroup().id))]",
      "location": "[resourceGroup().location]",
      "properties": {
        "databaseAccountOfferType": "Standard",
        "locations": [
          {
            "locationName": "westus"
          }
        ]
      }
    },
    {
      "type": "Microsoft.DocumentDB/databaseAccounts/sqlDatabases",
      "apiVersion": "2021-05-15",
      "name": "[concat('csmsarm', uniqueString(resourceGroup().id), '/cosmicworks')]",
      "dependsOn": [
        "[resourceId('Microsoft.DocumentDB/databaseAccounts', concat('csmsarm', uniqueString(resourceGroup().id)))]"
      ],
      "properties": {
        "resource": {
          "id": "cosmicworks"
        }
      }
    },
    {
      "type": "Microsoft.DocumentDB/databaseAccounts/sqlDatabases/containers",
      "apiVersion": "2021-05-15",
      "name": "[concat('csmsarm', uniqueString(resourceGroup().id), '/cosmicworks/products')]",
      "dependsOn": [
        "[resourceId('Microsoft.DocumentDB/databaseAccounts', concat('csmsarm', uniqueString(resourceGroup().id)))]",
        "[resourceId('Microsoft.DocumentDB/databaseAccounts/sqlDatabases', concat('csmsarm', uniqueString(resourceGroup().id)), 'cosmicworks')]"
      ],
      "properties": {
        "options": {
          "throughput": 400
        },
        "resource": {
          "id": "products",
          "partitionKey": {
            "paths": [
              "/categoryId"
            ]
          }
        }
      }
    }
  ]
}

```


## Learn more
-   [Quickstart: Create an Azure Cosmos DB and a container by using an ARM template](https://docs.microsoft.com/en-us/azure/cosmos-db/sql/quick-create-template)
-   [Bicep documentation](https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/)
-   [ARM template documentation](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/)
-   [Microsoft.DocumentDB | databaseAccounts](https://docs.microsoft.com/en-us/azure/templates/microsoft.documentdb/databaseaccounts)
-   [Microsoft.DocumentDB | databaseAccounts | sqldatabases](https://docs.microsoft.com/en-us/azure/templates/microsoft.documentdb/databaseaccounts/sqldatabases)
-   [Microsoft.DocumentDB | databaseAccounts | sqldatabases | containers](https://docs.microsoft.com/en-us/azure/templates/microsoft.documentdb/databaseaccounts/sqldatabases/containers)



-----
https://docs.microsoft.com/en-gb/learn/modules/create-resource-template-for-azure-cosmos-db-sql-api/