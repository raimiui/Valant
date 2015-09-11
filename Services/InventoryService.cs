using System;
using Valant.Model;
using Valant.Repositories.Interfaces;
using Valant.Services.Interfaces;

namespace Valant.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly INotificationService _notificationService;

        public InventoryService(IInventoryRepository inventoryRepository, INotificationService notificationService)
        {
            _inventoryRepository = inventoryRepository;
            _notificationService = notificationService;
        }

        public Inventory TakeOutByLabel(string label)
        {
            var item = _inventoryRepository.GetByLabel(label);
            if (item == null) return null;
            
            _inventoryRepository.Delete(item);

            var notification = new Notification{ Code = item.ExpirationDate < DateTime.Now ? NotificationCode.Expired : NotificationCode.ItemHasBeenTakenOut };
            _notificationService.Send(notification);

            return item;
        }

        public void Save(Inventory item)
        {
            _inventoryRepository.Save(item);
        }
    }
}
