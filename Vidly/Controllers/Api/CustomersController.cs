using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext context;

        public CustomersController()
        {
            context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetCustomers(string query = null)
        {
            //return context.Customers
            //    .Include(c=>c.MembershipType)
            //    .ToList()
            //    .Select(Mapper.Map<Customer, CustomerDto>);

            var customersQuery = context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();
            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            customerDto.Id = customer.Id;

            context.Customers.Add(customer);
            context.SaveChanges();

            //return customerDto;
            return Created(new Uri($"{Request.RequestUri}/{customer.Id}"), customerDto);
        }

        [HttpPut]
        public void UpdateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = context.Customers.SingleOrDefault(c => c.Id == customerDto.Id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);

            //customerInDb.Name = customerDto.Name;
            //customerInDb.Birthdate = customerDto.Birthdate;
            //customerInDb.IsSubscribedToNewsLetter = customerDto.IsSubscribedToNewsLetter;
            //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;

            context.SaveChanges();

            //return customerDto;
        }

        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            context.Customers.Remove(customerInDb);
            context.SaveChanges();
        }
    }
}
