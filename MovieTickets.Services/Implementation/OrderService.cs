using MovieTickets.Domain;
using MovieTickets.Repository.Interface;
using MovieTickets.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTickets.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

        public List<Movie> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return this._orderRepository.getOrderDetails(model);
        }
    }
}
