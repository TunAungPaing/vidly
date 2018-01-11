using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Customers
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MemberShipType).ToList();
            return View(customers);
        }
        //New Customer
        public ActionResult NewCustomer()
        {
            var membershiptypes = _context.MemberShipTypes.ToList();
            var viewModel = new NewCustomerViewModel
            {
                MemberShipTypes = membershiptypes
            };
            return View(viewModel);
        }

        // Customer Detail
        public ActionResult Detail(int id)
        {
            var customer = _context.Customers.Include(c => c.MemberShipType).SingleOrDefault(c => c.id == id);
            if (customer == null)
                return HttpNotFound();


            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MemberShipTypes = _context.MemberShipTypes.ToList()
                };
                return View("NewCustomer", viewModel);
            }
            if (customer.id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var _customerindb = _context.Customers.SingleOrDefault(m=>m.id==customer.id);
                _customerindb.name = customer.name;
                _customerindb.MemberShipTypeId = customer.MemberShipTypeId;
                _customerindb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                _customerindb.BirthDate = customer.BirthDate;
            }
            _context.SaveChanges();
            return RedirectToAction("Index","Customers");
        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(m => m.id == id);
            if (customer==null)
            {
                return HttpNotFound();
            }
            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MemberShipTypes = _context.MemberShipTypes.ToList()
            };
            return View("NewCustomer",viewModel);
        }
    }
}