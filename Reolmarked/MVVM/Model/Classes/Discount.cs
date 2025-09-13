using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reolmarked.MVVM.Model.Classes
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public string Type { get; set; }

        public double Rate { get; set; }


        public Discount(int discountId, string type, double rate)
        {
            DiscountId = discountId;
            Type = type;
            Rate = rate;
        }

   
    }
}
