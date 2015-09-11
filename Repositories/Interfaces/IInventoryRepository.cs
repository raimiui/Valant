using Valant.Model;

namespace Valant.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Inventory GetByLabel(string label);
        void Save(Inventory item);
        void Delete(Inventory item);
    }
}
