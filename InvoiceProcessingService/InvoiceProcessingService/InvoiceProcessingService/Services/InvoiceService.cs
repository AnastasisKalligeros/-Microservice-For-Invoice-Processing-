using InvoiceProcessingService.Data;
using InvoiceProcessingService.Models;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceProcessingService.Services
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _repository;

        public InvoiceService(InvoiceRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Invoice> GetInvoices()
        {
            return _repository.GetAllInvoices();
        }

        public Invoice GetInvoice(int id)
        {
            return _repository.GetInvoiceById(id);
        }

        public void CreateInvoice(Invoice invoice)
        {
            // Έλεγχος για ύπαρξη τιμολογίου με το ίδιο id
            if (_repository.GetAllInvoices().Any(i => i.InvoiceId == invoice.InvoiceId ))
            {
                throw new ArgumentException("Invoice with the same ID already exists.");
            }

            _repository.AddInvoice(invoice);
        }

        public void UpdateInvoice(int id, Invoice updatedInvoice)
        {
            var existingInvoice = _repository.GetInvoiceById(id);
            if (existingInvoice != null)
            {
                existingInvoice.CustomerName = updatedInvoice.CustomerName;
                existingInvoice.InvoiceDate = updatedInvoice.InvoiceDate;
                existingInvoice.TotalAmount = updatedInvoice.TotalAmount;
            }
        }

        public void DeleteInvoice(int id)
        {
            var existingInvoice = _repository.GetInvoiceById(id);
            if (existingInvoice != null)
            {
                _repository.RemoveInvoice(id);
            }
        }
    }
}
