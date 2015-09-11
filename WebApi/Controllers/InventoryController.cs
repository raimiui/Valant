using System.Web.Http;
using Valant.Model;
using Valant.Services.Interfaces;

namespace Valant.WebApi.Controllers
{
    public class InventoryController : ApiController
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public Inventory TakeOutByLabel(string label)
        {
            return _inventoryService.TakeOutByLabel(label);
        }

        [HttpPost]
        public void Add(Inventory item)
        {
            _inventoryService.Save(item);
        }
    }
}
