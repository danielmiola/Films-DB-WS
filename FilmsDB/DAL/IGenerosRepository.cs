using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmsDB.Models;

namespace FilmsDB.DAL
{
    interface IGenerosRepository : IDisposable
    {
        IEnumerable<Generos> Get();
        Generos GetByID(int id);
        void Insert(Generos genero);
        void Delete(int id);
        void Update(Generos genero);
        int Count(int id);
        void Save();
    }
}
