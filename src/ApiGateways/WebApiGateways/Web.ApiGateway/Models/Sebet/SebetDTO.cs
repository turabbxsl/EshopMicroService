using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ApiGateway.Models.Sebet
{
    public class SebetDTO
    {
        public string BuyerId { get; set; }

        public List<SebetItemDTO> SebetItems { get; set; } = new List<SebetItemDTO>();

        public SebetDTO()
        {

        }

        public SebetDTO(string buyerid)
        {
            BuyerId = buyerid;
        }

    }
}
