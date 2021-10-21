using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Utility.Order
{
    public static class BookPrice
    {
        public static double GetPriceBasedOnQuantity(int count, int price,
            int price50, int price100)
        {
            if (count < 50)
            {
                return price;
            }
            else
            {
                if (count < 100)
                {
                    return price50;
                }
                else
                {
                    return price100;
                }
            }
        }
    }
}
