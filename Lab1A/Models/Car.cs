using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1A.Models
{
    public class Car
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(1900,2018,ErrorMessage ="Please provide a year within a valid range.")]
        public int Year { get; set; }

        [Required]
        public string VIN { get; set; }

        public string Color { get; set; }

        public int DealershipID { get; set; }
    }
}
