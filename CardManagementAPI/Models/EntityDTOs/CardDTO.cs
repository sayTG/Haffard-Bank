using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardManagementAPI.Models.EntityDTOs
{
    public class CardDTO
    {
        [Required]
        public string CustomerID { get; set; }
        [Range(0000, 9999, ErrorMessage = "Maximum of 4 digits")]
        [Required]
        public int Pin { get; set; }
    }
}
