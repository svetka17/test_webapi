using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApplication.Models
{
    public class AppLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateLog { get; set; }

        [Required]
        public string Message { get; set; }
    }
}