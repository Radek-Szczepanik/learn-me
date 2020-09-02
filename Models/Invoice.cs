using System;

namespace LearnMeAPI.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateOfInvoice { get; set; }
        public DateTime DateOfPayment { get; set; }
        public decimal InvoiceAmount { get; set; }
    }
}
