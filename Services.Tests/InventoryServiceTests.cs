using System;
using NSubstitute;
using NUnit.Framework;
using Valant.Model;
using Valant.Repositories.Interfaces;
using Valant.Services.Interfaces;

namespace Valant.Services.Tests
{
    [TestFixture]
    public class InventoryServiceTests
    {
        IInventoryRepository _inventoryRepository;
        IInventoryService _inventoryService;
        INotificationService _notificationService;

        [SetUp]
        public void Setup()
        {
            _inventoryRepository = Substitute.For<IInventoryRepository>();
            _notificationService = Substitute.For<INotificationService>();
            _inventoryService = new InventoryService(_inventoryRepository, _notificationService);
        }

        [TestCase]
        public void InventoryService_ItemIsDeletedAfterTakingIt()
        {
            // Arrange
            var itemLabel = "Newest inventory " + Guid.NewGuid();
            var expirationDate = DateTime.Now;
            var inventoryItem = new Inventory { Label = itemLabel, ExpirationDate = expirationDate };
            _inventoryRepository.GetByLabel(Arg.Any<string>()).Returns(inventoryItem);

            // Act
            _inventoryService.TakeOutByLabel(itemLabel);

            // Assert
            _inventoryRepository.Received(1).GetByLabel(Arg.Is(itemLabel));
            _inventoryRepository.Received(1).Delete(Arg.Any<Inventory>());
        }

        [TestCase]
        public void InventoryService_NotificationSent_WhenItemIsNotExpiredAndTakenOut()
        {
            // Arrange
            var itemLabel = "Newest inventory " + Guid.NewGuid();
            var expirationDate = DateTime.Now.AddYears(+1);
            var inventoryItem = new Inventory { Label = itemLabel, ExpirationDate = expirationDate };
            _inventoryRepository.GetByLabel(Arg.Any<string>()).Returns(inventoryItem);

            // Act
            _inventoryService.TakeOutByLabel(itemLabel);

            // Assert
            _notificationService.Received(1).Send(Arg.Is<Notification>(x => x.Code == NotificationCode.ItemHasBeenTakenOut));
        }

        [TestCase(+1)]
        [TestCase(-1)]
        public void InventoryService_NotificationSent_WhenItemIsExpired(int addYears)
        {
            // Arrange
            var itemLabel = "Newest inventory " + Guid.NewGuid();
            var expirationDate = DateTime.Now.AddYears(addYears);
            var inventoryItem = new Inventory { Label = itemLabel, ExpirationDate = expirationDate };
            _inventoryRepository.GetByLabel(Arg.Any<string>()).Returns(inventoryItem);

            // Act
            _inventoryService.TakeOutByLabel(itemLabel);
            var expectedNotificationCode = expirationDate < DateTime.Now ? NotificationCode.Expired : NotificationCode.ItemHasBeenTakenOut;

            // Assert
            _notificationService.Received(1).Send(Arg.Is<Notification>(x => x.Code == expectedNotificationCode));
        }

        [TestCase]
        public void InventoryService_NoNotificationSending_WhenItemDoesNotExist()
        {
            // Arrange
            var itemLabel = "Newest inventory " + Guid.NewGuid();
            var expirationDate = DateTime.Now.AddYears(+1);
            var inventoryItem = new Inventory { Label = itemLabel, ExpirationDate = expirationDate };
            //_inventoryRepository.GetByLabel(Arg.Any<string>()).Returns(inventoryItem);

            // Act
            _inventoryService.TakeOutByLabel(itemLabel);

            // Assert
            _notificationService.DidNotReceiveWithAnyArgs().Send(Arg.Any<Notification>());
        }
    }
}
