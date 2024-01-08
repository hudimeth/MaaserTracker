using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactMaaserTrackerMUI_Starter.Data
{
    public class IncomeSource
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public List<Income> Incomes { get; set; }
    }
}
