using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Valant.Model;

namespace Valant.Repositories.Tests
{
    [TestFixture]
    public class InventoryRepositoriesTests
    {
        InventoryRepository _inventoryRepository;

        [SetUp]
        public void Setup()
        {
            _inventoryRepository = new InventoryRepository();
        }

        [TestCase]
        public void InventoryRepository_SavedItemContainsLabelAndExpirationDate()
        {
            // Arrange
            var itemLabel = "Newest inventory " + Guid.NewGuid();
            var expirationDate = DateTime.Now;
            var inventoryItem = new Inventory { Label = itemLabel, ExpirationDate = expirationDate };

            // Act
            _inventoryRepository.Save(inventoryItem);
            var inventoryItembyLabel = _inventoryRepository.GetByLabel(itemLabel);

            // Assert
            Assert.That(inventoryItembyLabel.Label, Is.EqualTo(itemLabel));
            Assert.That(inventoryItembyLabel.ExpirationDate, Is.EqualTo(expirationDate));
        }

        [TestCase]
        public void InventoryRepository_DeletesItem()
        {
            // Arrange
            var itemLabel = "Newest inventory " + Guid.NewGuid();
            var expirationDate = DateTime.Now;
            var inventoryItem = new Inventory { Label = itemLabel, ExpirationDate = expirationDate };

            // Act
            _inventoryRepository.Save(inventoryItem);
            _inventoryRepository.Delete(inventoryItem);
            var inventoryItembyLabel = _inventoryRepository.GetByLabel(itemLabel);

            // Assert
            Assert.That(inventoryItembyLabel, Is.Null);
        }
    }
}
