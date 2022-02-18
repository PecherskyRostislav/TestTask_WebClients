using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask_WebClients.EntityContext;
using TestTask_WebClients.Models;

namespace TestTask_WebClients.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomersApi : Controller
    {
        private readonly ContextDb _context;
        private readonly ILogger _logger;

        public CustomersApi(ContextDb context, ILogger<CustomersApi> logger)
        {
            _context = context;
            _logger = logger;
        }


        // GET: customer
        [HttpGet("")]
        public async Task<IActionResult> Customers()
        {
            List<Customer> customers;
            try
            {
                customers = await _context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CustomersController -> Index -> Get customers");
                customers = new List<Customer>();
            }
            return Json(customers);
        }


        // GET: customer/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Customers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Customer customer = null;
            try
            {
                customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
                if (customer == null)
                {
                    _logger.LogDebug($"CustomersController -> Details -> Сan't find customers by id {id}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CustomersController -> Details -> Сan't get Customer by id {id}");
            }

            return Json(customer);
        }


        // POST: customer
        [HttpPost("")]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var mailExist = await _context.Customers.AnyAsync(x => x.Emails.Equals(customer.Emails));
                if (mailExist)
                {
                    ModelState.AddModelError("Emails", "Customer with this email address already exists.");
                }
                else
                {
                    try
                    {
                        _context.Add(customer);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"CustomersController -> Create -> Can`t add customer '{JsonConvert.SerializeObject(customer)}'");
                    }

                    return Ok(customer);
                }
            }
            return BadRequest(customer);
        }


        // PUT: customer/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (customer == null || id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // if another customer have current email
                var mailExist = await _context.Customers.AnyAsync(x => x.Emails.Equals(customer.Emails) && x.Id != customer.Id);
                if (mailExist)
                {
                    ModelState.AddModelError("Emails", "Customer with this email address already exists.");
                }
                else
                {
                    try
                    {
                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        if (!CustomerExists(customer.Id))
                        {
                            _logger.LogError(ex, "CustomersController -> DbUpdateConcurrencyException -> Edit -> Update customer");
                            return NotFound();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "CustomersController -> Exception -> Edit -> Update customer");
                    }
                    return Ok(customer);
                }
            }
            return BadRequest(customer);
        }


        // DELETE: customer/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                _logger.LogDebug($"CustomersController -> DeleteConfirmed -> Сan't find customers by id {id}");
                return NotFound();
            }

            try
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CustomersController -> DeleteConfirmed -> Сan't delete customers by id {id}");
            }

            return Ok();
        }


        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
