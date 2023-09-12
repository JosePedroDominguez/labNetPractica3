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
    public class ProvedoresLogic : LogicBasic, IADGULogic<Suppliers>
    {
        public void Add(Suppliers supplier)
         {

             try
             {
                 context.Suppliers.Add(supplier);
                 context.SaveChanges();
             }
             catch (Exception)
             {
                 throw new DataException();
             }
         }

         public void Delete(int id)
         {

             var supplier = context.Suppliers.Find(id);
             if (supplier == null)
             {
                 throw new ExcepcionPersonalizada();
             }
             else
             {
                 context.Suppliers.Remove(supplier);
                 context.SaveChanges();
             }
         }

         public List<Suppliers> GetAll()
         {
             return context.Suppliers.ToList();
         }


         public void Update(Suppliers supplier)
         {
             var supplierActualizado = context.Suppliers.Find(supplier.SupplierID);
             if (supplierActualizado == null)
             {
                 throw new ExcepcionPersonalizada();
             }
             else
             {
                 try
                 {
                     supplierActualizado.CompanyName = supplier.CompanyName;
                     supplierActualizado.ContactName = supplier.ContactName;
                     supplierActualizado.ContactTitle = supplier.ContactTitle;
                     supplierActualizado.Address = supplier.Address;
                     supplierActualizado.City = supplier.City;
                     supplierActualizado.Country = supplier.Country;
                     supplierActualizado.SupplierID = supplier.SupplierID;
                     context.SaveChanges();
                 }
                 catch (Exception)
                 {
                     throw new DataException();
                 }
             }
         }

         public Suppliers Encontrar(int id)
        {
            var supplier = context.Suppliers.Find(id);
            if (supplier != null)
            {
                return supplier;
            }
            throw new ExcepcionPersonalizada();

        }

    }
}


