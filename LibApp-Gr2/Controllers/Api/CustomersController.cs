using AutoMapper;
using LibApp.Dtos;
using LibApp.Models;
using LibApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Web.Http;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace LibApp.Controllers.Api
{
    // kontroler przetwarzający informacje o klientach (API)
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly CustomerRepository repository;

        public CustomersController(IMapper mapper, CustomerRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        // POST /api/customers
        [HttpPost]
        public IActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            repository.Add(mapper.Map<Customer>(customerDto));
            repository.Save();

            return CreatedAtRoute(new { id = customerDto.Id }, customerDto);
        }

        // DELETE /api/customers
        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
            var customerInDb = repository.GetOne(id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Delete(id);
        }

        // GET /api/customers/{id}
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = repository.GetOne(id);

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(mapper.Map<CustomerDto>(customer));
        }

        // GET /api/customers
        [HttpGet]
        public IActionResult GetCustomers(string query = null)
        {
            var customers = repository.GetAll()
                .Where(c => query == null || c.Name.ToUpper().Contains(query.ToUpper()))
                .Select(c => mapper.Map<CustomerDto>(c))
                .ToList();

            return Ok(customers);
        }

        // PUT /api/customers
        [HttpPut("{id}")]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = repository.GetOne(id);

            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            customerInDb.Id = customerDto.Id;
            customerInDb.Birthdate = customerDto.Birthdate;
            customerInDb.HasNewsletterSubscribed = customerDto.HasNewsletterSubscribed;
            customerInDb.MembershipTypeId = customerDto.MembershipTypeId;
            customerInDb.Name = customerDto.Name;

            repository.Save();
        }
    }
}
