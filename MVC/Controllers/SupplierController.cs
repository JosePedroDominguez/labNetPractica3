using Lab.Practica.EF.Logic;
using Lab.Practica.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC.Models;
using PagedList;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class SupplierController : Controller
    {
        readonly ProvedoresLogic logic = new ProvedoresLogic();

        public ActionResult Index()
        {

            List<Suppliers> suppliers = logic.GetAll();

            List<SupplierView> supplierView = suppliers.Select(s => new SupplierView
            {
                Id = s.SupplierID,
                CompanyName = s.CompanyName,
                ContactName = s.ContactName,
                ContactTitle = s.ContactTitle,
                Address = s.Address,
                City = s.City,
                Country = s.Country,
            }).ToList();
            return View(supplierView);
        }

        public ActionResult InsertUpdate(int? id)
        {

            SupplierView supplier = new SupplierView();
            if (id != null)
            {
                var sup = logic.Encontrar((int)id);
                supplier.CompanyName = sup.CompanyName;
                supplier.ContactName = sup.ContactName;
                supplier.ContactTitle = sup.ContactTitle;
                supplier.Address = sup.Address;
                supplier.City = sup.City;
                supplier.Country = sup.Country;

            }
            return View(supplier);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertUpdate(SupplierView supplierView)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("index", "Error");
                }
                else
                {
                    Suppliers supplierEntity = new Suppliers
                    {
                        SupplierID = supplierView.Id,
                        CompanyName = supplierView.CompanyName,
                        ContactName = supplierView.ContactName,
                        ContactTitle = supplierView.ContactTitle,
                        Address = supplierView.Address,
                        City = supplierView.City,
                        Country = supplierView.Country,
                    };

                    if (supplierView.Id == 0)
                    {
                        logic.Add(supplierEntity);
                        return RedirectToAction("index");
                    }
                    else
                    {
                        logic.Update(supplierEntity);
                        return RedirectToAction("index");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("index", "Error", new { mssg = ex.Message });
            }
        }
        [HttpGet]
        public bool Delete(int id)
        {
            try
            {
                logic.Delete(id);
                return true;
            }
            catch
            {
                Response.StatusCode = 422;
                return false;
            }
        }

    }
}
