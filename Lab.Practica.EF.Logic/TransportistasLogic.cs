using Lab.Practica.EF.Data;
using Lab.Practica.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Practica.EF.Logic
{
    public class TransportistasLogic : LogicBasic, IADGULogic<Shippers>
    {
        public void Add(Shippers shippers)
        {

            try
            {
                context.Shippers.Add(shippers);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw new DataException();
            }
        }

        public void Delete(int id)
        {

            var shippers = context.Shippers.Find(id);
            if (shippers == null)
            {
                throw new ExcepcionPersonalizada();
            }
            else
            {
                context.Shippers.Remove(shippers);
                context.SaveChanges();
            }
        }

        public List<Shippers> GetAll()
        {
            return context.Shippers.ToList();
        }


        public void Update(Shippers shippers)
        {
            var ShipperActualizado = context.Shippers.Find(shippers.ShipperID);
            if (ShipperActualizado == null)
            {
                throw new ExcepcionPersonalizada();
            }
            else
            {
                try
                {
                    ShipperActualizado.CompanyName = shippers.CompanyName;
                    ShipperActualizado.Phone = shippers.Phone;
                    ShipperActualizado.ShipperID = shippers.ShipperID;
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new DataException();
                }
            }
        }

        public Shippers Encontrar(int id)
        {
            var shippers = context.Shippers.Find(id);
            if (shippers != null)
            {
                throw new ExcepcionPersonalizada();
            }
            return shippers;

        }
        public List<Shippers> SelecTop(int value)
        {
            var shippers = (from shipper in context.Shippers
                            select shipper).Take(value);
            return shippers.ToList();
        }

    }

 }





