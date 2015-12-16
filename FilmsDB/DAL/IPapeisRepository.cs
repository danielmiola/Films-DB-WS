using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmsDB.Models;

namespace FilmsDB.DAL
{
    interface IPapeisRepository : IDisposable
    {
        IEnumerable<Papeis> Get();
        Papeis GetByID(int Fid, int Aid);
        void Insert(Papeis papel);
        void Delete(int Fid, int Aid);
        void Update(Papeis papel);
        int Count(int Fid, int Aid);
        void Save();
    }
}
