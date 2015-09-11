using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valant.Model;

namespace Valant.Services.Interfaces
{
    public interface IInventoryService
    {
        Inventory TakeOutByLabel(string label);
        void Save(Inventory item);
    }
}
