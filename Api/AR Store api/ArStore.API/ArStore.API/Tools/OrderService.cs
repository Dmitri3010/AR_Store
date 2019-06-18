using System;
using ArStore.API.Models;
using ArStore.API.Models.API;
using ArStore.API.Repository;

namespace ArStore.API.Tools
{
    public static class OrderService
    {
        public static void AddOrder(OrderApiRequest request)
        {
            try
            {
                Order order = new Order
                {
                    Name = request.Name,
                    Phone = request.Phone,
                    Date = DateTime.Now,
                    ProductId = request.ProductId,
                    Adress =  request.Adress
                };

                Orders.AddOrUpdate(order);
            }
            catch (Exception ex)
            {
            }
        }
    }
}