using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask_WebClients.Models
{
    [Table("Customers")]
    [Index("Emails", IsUnique = true)]
    public class Customer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        [Column("emails")]
        public string Emails { get; set; }

        /// <summary>
        /// true - is female, false - is male
        /// </summary>
        [Column("gender")]
        public bool Gender { get; set; }

        [Column("day_of_birth")]
        [Required]
        public DateTime DayOfBirth { get; set; }
    }
}
