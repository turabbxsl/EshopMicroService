using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Application.Services.DTOs
{
    public class SebetDTO
    {
        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }


        public string ZipCode { get; set; }


        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        [Required]
        public string CardExpirationSort { get; set; }

        [Required]
        public DateTime CardExpiration { get; set; }

        [Required]
        public string CardSecurityNumber { get; set; }


        public int CartTypeId { get; set; }
        public string Buyer { get; set; }

    }
}
