using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmsDB.Models;

namespace FilmsDB.DAL
{
    interface IReviewsRepository : IDisposable
    {
        IEnumerable<Reviews> Get();
        Reviews GetByID(int id);
        void Insert(Reviews review);
        void Delete(int id);
        void Update(Reviews review);
        int Count(int id);
        void Save();
    }
}
