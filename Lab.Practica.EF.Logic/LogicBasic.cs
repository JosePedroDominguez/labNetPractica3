using Lab.Practica.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Practica.EF.Logic
{
    public class LogicBasic
    {
        protected readonly NorthWindContext context;
        public LogicBasic()
        {
            context = new NorthWindContext();
        }

    }
}
