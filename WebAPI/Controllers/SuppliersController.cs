using Lab.Practica.EF.Entities;
using Lab.Practica.EF.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models; 

namespace MVC.Controllers
{
    public class SuppliersController : ApiController
    {
        private ProvedoresLogic logic = new ProvedoresLogic();

       
        public IHttpActionResult Get()
        {
            try
            {
               
                List<Suppliers> suppliers = logic.GetAll();
                List<SuppliersDTO> suppliersDTOs = suppliers.Select(s => new SuppliersDTO
                {
                    Id = s.SupplierID,
                    CompanyName = s.CompanyName,
                    ContactName = s.ContactName,
                    ContactTitle = s.ContactTitle,
                    Address = s.Address,
                    City = s.City,
                    Country = s.Country
                }).ToList();

                return Ok(suppliersDTOs);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

      
        public IHttpActionResult Get(int id)
        {
            try
            {
                var supplier = logic.Encontrar(id);
                if (supplier == null)
                    return NotFound();

              
                SuppliersDTO supplierDTO = new SuppliersDTO
                {
                    Id = supplier.SupplierID,
                    CompanyName = supplier.CompanyName,
                    ContactName = supplier.ContactName,
                    ContactTitle = supplier.ContactTitle,
                    Address = supplier.Address,
                    City = supplier.City,
                    Country = supplier.Country
                };

                return Ok(supplierDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

       
        public IHttpActionResult Post([FromBody] SuppliersDTO supplierDTO)
        {
            try
            {
                if (supplierDTO == null)
                    return BadRequest("Invalid supplier data.");

               
                Suppliers supplier = new Suppliers
                {
                    SupplierID = supplierDTO.Id,
                    CompanyName = supplierDTO.CompanyName,
                    ContactName = supplierDTO.ContactName,
                    ContactTitle = supplierDTO.ContactTitle,
                    Address = supplierDTO.Address,
                    City = supplierDTO.City,
                    Country = supplierDTO.Country
                };

                logic.Add(supplier);
                return Created(new Uri(Request.RequestUri + "/" + supplier.SupplierID), supplierDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult Put(int id, [FromBody] SuppliersDTO supplierDTO)
        {
            try
            {
                if (supplierDTO == null || id != supplierDTO.Id)
                    return BadRequest("Invalid supplier data.");

                var existingSupplier = logic.Encontrar(id);
                if (existingSupplier == null)
                    return NotFound();

             
                existingSupplier.CompanyName = supplierDTO.CompanyName;
                existingSupplier.ContactName = supplierDTO.ContactName;
                existingSupplier.ContactTitle = supplierDTO.ContactTitle;
                existingSupplier.Address = supplierDTO.Address;
                existingSupplier.City = supplierDTO.City;
                existingSupplier.Country = supplierDTO.Country;

                logic.Update(existingSupplier);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult Delete(int id)
        {
            try
            {
                var existingSupplier = logic.Encontrar(id);
                if (existingSupplier == null)
                    return NotFound();

                logic.Delete(id);



                return Ok(existingSupplier);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
