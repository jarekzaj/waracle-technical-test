provider "azurerm" {
  features {}
}

terraform {
  backend "azurerm" {
    container_name = "terraform"
    key            = "terraform.tfstate"
  }
}