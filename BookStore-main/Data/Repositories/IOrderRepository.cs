﻿using BookStore.Data.ViewModels;
using BookStore.Models;

namespace BookStore.Data.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        void StoreOrder(List<ShoppingCartItem> items, string userId, ShippingInfo shippingInfo);
        List<Order> GetOrderByUserIdAndUserRole(string userId, string userRole);
        void CancelOrder(string userId, int orderId);
        void CancelOrderByAdmin(int orderId);
        void OrderPaid(int orderId);
        PaymentMethodDropDown GetDropDownValues();

    }
}
