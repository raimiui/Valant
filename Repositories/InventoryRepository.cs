using System.Collections.Generic;
using System.Linq;
using Valant.Model;
using Valant.Repositories.Interfaces;

namespace Valant.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private static readonly IList<Inventory> InventoryItemList = new List<Inventory>();

        public Inventory GetByLabel(string label)
        {
            return InventoryItemList.FirstOrDefault(x => x.Label == label);
        }

        public void Save(Inventory item)
        {
            InventoryItemList.Add(item);
        }

        public void Delete(Inventory item)
        {
            InventoryItemList.Remove(item);
        }
    }
}
