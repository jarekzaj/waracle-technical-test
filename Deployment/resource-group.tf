resource "azurerm_resource_group" "tf" {
  name = "WaracleTest-${var.ENV}"
  location = "UK South"
}
