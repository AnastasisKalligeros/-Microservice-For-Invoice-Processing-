using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InvoiceProcessingService.Services;
using InvoiceProcessingService.Models;

namespace InvoiceProcessingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(InvoiceService invoiceService, ILogger<InvoiceController> logger)
        {
            _invoiceService = invoiceService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetInvoices()
        {
            _logger.LogInformation("Ανάκτηση όλων των τιμολογίων");
            var invoices = _invoiceService.GetInvoices();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoice(int id)
        {
            _logger.LogInformation($"Ανάκτηση τιμολογίου με ID: {id}");
            var invoice = _invoiceService.GetInvoice(id);
            if (invoice == null)
                return NotFound();
            return Ok(invoice);
        }

        [HttpPost]
        public IActionResult CreateInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation($"Δημιουργία νέου τιμολογίου για τον πελάτη: {invoice.CustomerName}");
            _invoiceService.CreateInvoice(invoice);
            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.InvoiceId }, invoice);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInvoice(int id, Invoice invoice)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation($"Ενημέρωση τιμολογίου με ID: {id}");
            var existingInvoice = _invoiceService.GetInvoice(id);
            if (existingInvoice == null)
                return NotFound();

            _invoiceService.UpdateInvoice(id, invoice);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(int id)
        {
            _logger.LogInformation($"Διαγραφή τιμολογίου με ID: {id}");
            var existingInvoice = _invoiceService.GetInvoice(id);
            if (existingInvoice == null)
                return NotFound();

            _invoiceService.DeleteInvoice(id);
            return NoContent();
        }
    }
}
