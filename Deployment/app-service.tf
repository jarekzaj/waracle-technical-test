resource "azurerm_app_service_plan" "tf" {
  name                = "waracletest-appserviceplan-${var.ENV}"
  location            = azurerm_resource_group.tf.location
  resource_group_name = azurerm_resource_group.tf.name

  kind     = "Windows"
  reserved = false
  sku {
    tier = "Standard"
    size = "S1"
  }
  tags = {
    environment = "${var.ENV}"
  }
}

resource "azurerm_app_service" "tf" {
  name                = "waracletest-${var.ENV}"
  location            = azurerm_resource_group.tf.location
  resource_group_name = azurerm_resource_group.tf.name
  app_service_plan_id = azurerm_app_service_plan.tf.id
  https_only          = true
  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY"  = azurerm_application_insights.app_insights.instrumentation_key
    "CosmosDbConfiguration__ConnectionString" = azurerm_cosmosdb_account.tf.connection_strings[0]
  }

  tags = {
    environment = "${var.ENV}"
  }
}
