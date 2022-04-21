resource "azurerm_application_insights" "app_insights" {
  name                = "waracletestappinsights-${var.ENV}"
  location            = azurerm_resource_group.tf.location
  resource_group_name = azurerm_resource_group.tf.name
  application_type    = "web"
  tags                = {
    environment = "${var.ENV}"
  }
}

output "instrumentation_key" {
  value = azurerm_application_insights.app_insights.instrumentation_key
}