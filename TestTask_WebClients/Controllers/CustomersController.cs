using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TestTask_WebClients.EntityContext;
using TestTask_WebClients.Models;

namespace TestTask_WebClients.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ContextDb _context;
        private readonly ILogger _logger;

        public CustomersController(ContextDb context, ILogger<CustomersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Customers
        [HttpGet]
        public async Task<IActionResult> Index()
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
            return View(customers);
        }

        // GET: Customers/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
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

            return View(customer);
        }

        // GET: Customers/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Emails,Gender,DayOfBirth")] Customer customer)
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

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = null;
            try
            {
                customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    _logger.LogDebug($"CustomersController -> Edit -> Сan't find customers by id {id}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CustomersController -> Edit -> Сan't get Customer by id {id}");
            }

            return View(customer);
        }

        // Put: Customers/Edit/5
        [HttpPut]
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
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, "CustomersController -> Exception -> Edit -> Update customer");
                    }
                    return Ok();
                } 
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
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
                    _logger.LogDebug($"CustomersController -> Delete -> Сan't find customers by id {id}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CustomersController -> Delete -> Сan't get Customer by id {id}");
            }

            return View(customer);
        }

        // Delete: Customers/Delete/5
        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
