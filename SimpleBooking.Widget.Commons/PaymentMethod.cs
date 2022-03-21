using System.ComponentModel;

namespace SimpleBookingWidget.Commons
{
    public enum PaymentMethod
    {
        [Description("Cash")]
        Cash = 1,
        [Description("Credit Card")]
        CreditCard = 2,
        [Description("Bank Transfer")]
        BankTransfer = 3,
        [Description("FOC")]
        FOC = 4,
    }
}
