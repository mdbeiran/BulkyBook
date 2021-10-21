using BulkyBook.DomainClass.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.ViewModel.Order
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> CartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
