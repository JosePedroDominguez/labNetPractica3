using Lab.Practica.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab.Practica.EF.Logic
{
    public interface IBossesService
    {
        List<Boss> GetBosses();
    }
}
