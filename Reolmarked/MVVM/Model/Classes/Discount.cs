namespace Reolmarked.MVVM.Model.Classes
{
        //[Description("Mængderabat 2-3 reoler")]
        //[Description("Særskilt rabat - faste lejere")]
        //[Description("10% Kampagne")]
        //[Description("Særskilt rabat - anden")]

    public class Discount
    {
        public int DiscountId { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }


        public Discount(int discountId, string description, double rate)
        {
            DiscountId = discountId;
            Description = description;
            Rate = rate;
        }

        public Discount(string description, double rate)
        {
            Description = description;
            Rate = rate;
        }
    }
}
