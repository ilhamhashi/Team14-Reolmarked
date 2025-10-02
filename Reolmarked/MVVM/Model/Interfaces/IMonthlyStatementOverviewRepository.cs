using Reolmarked.MVVM.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reolmarked.MVVM.Model.Interfaces
{
    public interface IMonthlyStatementOverviewRepository
    {
        IEnumerable<MonthlyStatementItem> GetOverview();
        //MonthlyStatementItem GetById(int id);
    }
}
