using BulkyBook.DomainClass.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.ViewModel.Order
{
    public class OrderDetailsVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
