using System;
using System.ComponentModel.DataAnnotations;

namespace InvoiceProcessingService.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        [Required(ErrorMessage = "Το όνομα πελάτη είναι υποχρεωτικό.")]
        public string CustomerName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Το ποσό πρέπει να είναι θετικό.")]
        public decimal TotalAmount { get; set; }
    }
}
