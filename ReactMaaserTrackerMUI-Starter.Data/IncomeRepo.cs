using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactMaaserTrackerMUI_Starter.Data
{
    public class IncomeRepo
    {
        private string _connectionString;
        public IncomeRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddIncome(Income income)
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            ctx.Incomes.Add(income);
            ctx.SaveChanges();
        }
        public List<Income> GetAllIncomes()
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            return ctx.Incomes.ToList();
        }

        public List<IncomeSource> GetSourcesWithIncomes()
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            return ctx.Sources.Include(source => source.Incomes).ToList();
        }

        public string GetSourceName(int sourceId)
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            return ctx.Sources.FirstOrDefault(s => s.Id == sourceId).Source;
        }
    }
}
