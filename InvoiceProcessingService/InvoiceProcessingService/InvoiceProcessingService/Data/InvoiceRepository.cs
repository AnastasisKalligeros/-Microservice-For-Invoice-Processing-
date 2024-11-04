using InvoiceProcessingService.Models;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceProcessingService.Data
{
    public class InvoiceRepository
    {
        private static List<Invoice> invoices = new List<Invoice>();

        public IEnumerable<Invoice> GetAllInvoices()
        {
            return invoices;
        }

        public Invoice GetInvoiceById(int id)
        {
            return invoices.FirstOrDefault(i => i.InvoiceId == id);
        }

        public void AddInvoice(Invoice invoice)
        {
            invoices.Add(invoice);
        }

        public void RemoveInvoice(int id)
        {
            var invoice = invoices.FirstOrDefault(i => i.InvoiceId == id);
            if (invoice != null)
            {
                invoices.Remove(invoice);
            }
        }
    }
}
