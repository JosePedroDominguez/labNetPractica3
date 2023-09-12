using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Practica.EF.Logic
{
    interface IADGULogic<T>
    {
        void Add(T data);

        void Delete(int id);

        List<T> GetAll();

        void Update(T data);

    }
}
