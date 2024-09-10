using SDSDemo.Models;
using System.Collections.Generic;

namespace SDSDemo.Interfaces
{
    public interface IRecyclableItemRepository
    {
        IEnumerable<RecyclableItem> GetAll();
        void Add(RecyclableItem recyclableItem);
        RecyclableItem GetById(int id);
        void Update(RecyclableItem recyclableItem);
        void Delete(int id);
        IEnumerable<RecyclableType> GetRecyclableTypes();
    }
}
