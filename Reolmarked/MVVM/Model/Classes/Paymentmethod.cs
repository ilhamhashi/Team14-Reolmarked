using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reolmarked.MVVM.Model.Classes
{
    public class PaymentMethod

    {
    public int PaymentMethodId { get; set; }
    public string Name { get; set; }

    public double Fee { get; set; }


    public PaymentMethod(int paymentMethodId, string name, double fee)
    {

        PaymentMethodId = paymentMethodId;
        Name = name;
        Fee = fee;

    }

 
}
}