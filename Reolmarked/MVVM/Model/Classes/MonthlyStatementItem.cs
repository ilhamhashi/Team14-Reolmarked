using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reolmarked.MVVM.Model.Classes
{
    public class MonthlyStatementItem
    {
        public int AgreementId { get; set; }
        public string RenterName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public decimal RentalPrice { get; set; }
        public string ShelfNumbers { get; set; }
    }
}
