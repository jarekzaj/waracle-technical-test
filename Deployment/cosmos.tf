resource "azurerm_cosmosdb_account" "tf" {
  name                      = "waracletest-cosmosdb-${var.ENV}"
  location                  = azurerm_resource_group.tf.location
  resource_group_name       = azurerm_resource_group.tf.name
  offer_type                = "Standard"
  kind                      = "GlobalDocumentDB"

  consistency_policy {
    consistency_level = "Session"
  }
}

resource "azurerm_cosmosdb_sql_database" "tf" {
  name                = "waracletestdb"
  resource_group_name = azurerm_cosmosdb_account.tf.resource_group_name
  account_name        = azurerm_cosmosdb_account.tf.name
}

resource "azurerm_cosmosdb_sql_container" "tf" {
  name                = "waracletestcontainer"
  resource_group_name = azurerm_cosmosdb_account.tf.resource_group_name
  account_name        = azurerm_cosmosdb_account.tf.name
  database_name       = azurerm_cosmosdb_sql_database.tf.name
  partition_key_path  = "/PartitionKey"
}

output "cosmos_connection_string" {
    value = azurerm_cosmosdb_account.tf.connection_strings[0]
    sensitive   = true
}