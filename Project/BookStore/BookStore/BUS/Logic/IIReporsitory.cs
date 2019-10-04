using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BUS.Logic
{
    public interface IReporsitory<Obj, Key, Response>
    {
        Task<Response> GetList();
        Task<Response> Insert(Obj obj);
        Task<Response> Delete(Obj obj);
        Task<Response> Update(Obj obj);
        Task<Response> GetObject(Key id);
    }
}
