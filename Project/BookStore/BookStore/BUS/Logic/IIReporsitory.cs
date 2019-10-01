using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BUS.Logic
{
    public interface IReporsitory<T, V>
    {
        Task<List<T>> GetList();
        Task<int> Insert(T obj);
        Task<int> Delete(T obj);
        Task<int> Update(T obj);
        Task<T> GetObject(V obj);
    }
}
