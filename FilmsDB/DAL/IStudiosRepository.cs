using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmsDB.Models;

namespace FilmsDB.DAL
{
    interface IStudiosRepository: IDisposable
    {
        IEnumerable<Studios> Get();
        Studios GetByID(int id);
        void Insert(Studios studio);
        void Delete(int id);
        void Update(Studios studio);
        int Count(int id);
        void Save();
    }
}
