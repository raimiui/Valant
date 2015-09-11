using System;
using System.Data.SqlTypes;
using Autofac;
using NUnit.Framework;
using Valant.Model;
using Valant.Services.Interfaces;
using Valant.WebApi.Configs;
using Valant.WebApi.Controllers;

namespace Valant.WebApi.Tests
{
    [TestFixture]
    public class InventoryControllerTests
    {
        private InventoryController _inventoryController;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
        }

        [SetUp]
        public void Setup()
        {
            var inventoryService = AutofacConfig.Container.Resolve<IInventoryService>();
            _inventoryController = new InventoryController(inventoryService);
        }

        [Test]
        public void InventoryController_TakeOutByLabel_ReturnsNothing_WhenNoAddedInventoryExists()
        {
            // Arrange
            var itemLabel = "Unexisting inventory " + Guid.NewGuid();

            // Act
            var result = _inventoryController.TakeOutByLabel(itemLabel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [TestCase(+1)]
        [TestCase(-1)]
        public void InventoryController_TakeOutByLabel_ReturnsInventoryItem_WhenAddedInventoryExists(int addYears)
        {
            // Arrange
            var itemLabel = "Newest inventory " + Guid.NewGuid();
            var expirationDate = DateTime.Now.AddYears(addYears);
            var inventoryItem = new Inventory { Label = itemLabel, ExpirationDate = expirationDate };

            // Act
            _inventoryController.Add(inventoryItem);
            var result = _inventoryController.TakeOutByLabel(itemLabel);

            // Assert
            Assert.That(result.Label, Is.EqualTo(itemLabel));
        }

        [Test]
        public void InventoryController_TakeOutByLabel_ReturnsNothing_WhenAddedInventoryWasTakenBefore()
        {
            // Arrange
            var itemLabel = "Newest inventory " + Guid.NewGuid();
            var expirationDate = DateTime.Now;
            var inventoryItem = new Inventory { Label = itemLabel, ExpirationDate = expirationDate };

            // Act
            _inventoryController.Add(inventoryItem);
            _inventoryController.TakeOutByLabel(itemLabel);
            var result = _inventoryController.TakeOutByLabel(itemLabel);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
