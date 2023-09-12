using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lab.Practica.EF.Data;
using Lab.Practica.EF.Entities;
using WebAPI.Models;

namespace WebApi.Controllers
{
    public class ShippersController : ApiController
    {
        private readonly NorthWindContext _context = new NorthWindContext();

       
        public IEnumerable<ShipperDTO> Get()
        {
            
            var shippers = _context.Shippers.Select(s => new ShipperDTO
            {
                Id = s.ShipperID,
                CompanyName = s.CompanyName,
                Phone = s.Phone
            }).ToList();

            return shippers;
        }

      
        public IHttpActionResult Get(int id)
        {
            var shipper = _context.Shippers.FirstOrDefault(s => s.ShipperID == id);
            if (shipper == null)
                return NotFound();

            
            var shipperDto = new ShipperDTO
            {
                Id = shipper.ShipperID,
                CompanyName = shipper.CompanyName,
                Phone = shipper.Phone
            };

            return Ok(shipperDto);
        }

     
        public IHttpActionResult Post([FromBody] ShipperDTO shipperDto)
        {
            if (shipperDto == null)
                return BadRequest();

           
            var shipper = new Shippers
            {
                ShipperID = shipperDto.Id,
                CompanyName = shipperDto.CompanyName,
                Phone = shipperDto.Phone
            };

            _context.Shippers.Add(shipper);
            _context.SaveChanges();

          
            return CreatedAtRoute("DefaultApi", new { id = shipper.ShipperID }, shipperDto);
        }

       
        public IHttpActionResult Put(int id, [FromBody] ShipperDTO shipperDto)
        {
            if (shipperDto == null || id != shipperDto.Id)
                return BadRequest();

            var existingShipper = _context.Shippers.FirstOrDefault(s => s.ShipperID == id);
            if (existingShipper == null)
                return NotFound();

            
            existingShipper.CompanyName = shipperDto.CompanyName;
            existingShipper.Phone = shipperDto.Phone;

            _context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(int id)
        {
            var shipper = _context.Shippers.FirstOrDefault(s => s.ShipperID == id);
            if (shipper == null)
                return NotFound();

            _context.Shippers.Remove(shipper);
            _context.SaveChanges();

           
            var shipperDto = new ShipperDTO
            {
                Id = shipper.ShipperID,
                CompanyName = shipper.CompanyName,
                Phone = shipper.Phone
            };

            return Ok(shipperDto);
        }
    }
}
