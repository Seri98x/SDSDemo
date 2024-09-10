using SDSDemo.Models;
using System.Collections.Generic;

namespace SDSDemo.Interfaces
{
    public interface IRecyclableTypeRepository
    {

        IEnumerable<RecyclableType> GetAll();
        void Add(RecyclableType recyclableType);
        RecyclableType GetById(int id);
        void Update(RecyclableType recyclableType);
        void Delete(int id);
    }
}
