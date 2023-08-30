using Mc2.Crud.Services;
using Mc2.CrudTest.Client.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Client.Controllers
{
    public class CustomerController : BaseMvcController<CustomerService>
    {
        private readonly CustomerService _customerService;
        public CustomerController(CustomerService customerService, ILogger<CustomerService> logger) : base(logger, 100)
        {
            _customerService = customerService;
        }

        // GET: CustomerController
        public async Task<IActionResult> Index()
        {   
            var b = await _customerService.ItemsAsync();
            LogMultipleGet(LogPurposeType.Success, "GetAllCustomers", null, 101);
            return View(b);
        }

        // GET: CustomerController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerService.RetrieveByIdAsync(id);
            return View(customer);
        }

        // GET: CustomerController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.RetrieveByIdAsync(id);
            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, string codeCountry, IFormCollection collection)
        {
            try
            {
                var customer = await _customerService.RetrieveByIdAsync(id);
                await _customerService.ModifyAsync(customer, codeCountry);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET: CustomerController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.RetrieveByIdAsync(id);
            return View(customer);
        }

        // Delete: CustomerController/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                await _customerService.RemoveByIdAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
