using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ApiGateway.Models.Sebet
{
    public class AddSebetItemRequest
    {

        public int CatalogItemId { get; set; }

        public string SebetId { get; set; }

        public int Quantity { get; set; }

        public AddSebetItemRequest()
        {
            Quantity = 1;
        }



    }
}
