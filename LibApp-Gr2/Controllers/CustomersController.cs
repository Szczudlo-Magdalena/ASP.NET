using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using LibApp.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace LibApp.Controllers
{
    [Authorize(Roles = "StoreManager,Owner")]
    public class CustomersController : Controller
    {
        private readonly RestApiClient api;

        public CustomersController(RestApiClient api)
        {
            this.api = api;
        }

        public ViewResult Index()
        {             
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = await api.GetOneCustomer(id);

            if (customer == null)
            {
                return Content("User not found");
            }

            return View(customer);
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> New()
        {
            var membershipTypes = await api.GetAllMembershipTypes();

            var viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await api.GetOneCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = await api.GetAllMembershipTypes()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Save(CustomerDto customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel(customer)
                {
                    MembershipTypes = await api.GetAllMembershipTypes()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                await api.AddCustomer(customer);
            }
            else
            {
                await api.UpdateCustomer(customer.Id, customer);
            }

            return RedirectToAction("Index", "Customers");
        }
    }
}