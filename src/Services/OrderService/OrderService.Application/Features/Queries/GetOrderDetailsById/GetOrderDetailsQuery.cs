﻿using MediatR;
using OrderService.Application.Features.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Queries.GetOrderDetailsById
{
    public class GetOrderDetailsQuery : IRequest<OrderDetailViewModel>
    {
        public Guid OrderId { get; set; }


        public GetOrderDetailsQuery(Guid orderid)
        {
            OrderId = orderid;
        }

    }
}
