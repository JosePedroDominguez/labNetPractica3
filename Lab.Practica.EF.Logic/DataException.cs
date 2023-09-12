using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Practica.EF.Logic
{
    public class DataException : Exception
    {
        public override string Message => "El dato ingresado supero el limite de caracteres";

    }
}
