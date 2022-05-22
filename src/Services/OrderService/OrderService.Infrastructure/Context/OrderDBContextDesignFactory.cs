using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Context
{
    public class OrderDBContextDesignFactory : IDesignTimeDbContextFactory<OrderDBContext>
    {

        public OrderDBContextDesignFactory()
        {

        }


        public OrderDBContext CreateDbContext(string[] args)
        {
            var connStr = "Server=c_sqlserver;Database=SellingMS;Trusted_Connection=True;";

            var optionBuilder = new DbContextOptionsBuilder<OrderDBContext>().UseSqlServer(connStr);

            return new OrderDBContext(optionBuilder.Options, new NoMediator());
        }


        class NoMediator : IMediator
        {
            public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public IAsyncEnumerable<object> CreateStream(object request, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult<TResponse>(default);
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult<object>(default);
            }
        }
    }
}
