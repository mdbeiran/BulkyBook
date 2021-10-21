using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Business.StaticTools
{
    public static class SD
    {
        #region Roles

        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
        public const string Role_User_Indi = "Individual Customer";
        public const string Role_User_Comp = "Company Customer";

        #endregion

        #region Session

        public const string ssShoppingCart = "Shopping Cart Session";

        #endregion

        #region Status

        // Order Status
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        // Payment Status
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";

        #endregion
    }
}
