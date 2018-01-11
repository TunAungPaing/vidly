using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //Get Api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        //Get Api/Customers/1
        public Customer GetSingleCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c=>c.id==id);
            if (customer==null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return customer;
        }

        //Post Api/Customers
        [HttpPost]
        public  Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        //Put Api/Customers/1
        [HttpPut]
        public void UpdateCustomer(int id,Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.id == id);
            if (CustomerInDb==null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            CustomerInDb.name = customer.name;
            CustomerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            CustomerInDb.BirthDate = customer.BirthDate;
            CustomerInDb.MemberShipTypeId = customer.MemberShipTypeId;
            _context.SaveChanges();
        }

        //Delete Api/Customer/1
        public void DeleteCustomer(int id)
        {
            var CustomerInDb = _context.Customers.SingleOrDefault(c => c.id == id);
            if (CustomerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _context.Customers.Remove(CustomerInDb);
            _context.SaveChanges();
        }
    }
}
