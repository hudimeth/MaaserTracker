using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ReactMaaserTrackerMUI_Starter.Data
{
    public class MaaserTrackerRepo
    {
        private string _connectionString;
        public MaaserTrackerRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddSource(string sourceName)
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            ctx.Sources.Add(new()
            {
                Source = sourceName
            });
            ctx.SaveChanges();
        }

        public void EditSource(int id, string sourceNameEdited)
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"UPDATE Sources SET Source = {sourceNameEdited} WHERE Id = {id}");
            ctx.SaveChanges();
        }

        public bool SourceHasIncomes(int sourceId)
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            return ctx.Sources.Include(s => s.Incomes).FirstOrDefault(s => s.Id == sourceId).Incomes.Count > 0;
        }

        public void DeleteSource(int id)
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            if (SourceHasIncomes(id))
            {
                return;
            }
            ctx.Database.ExecuteSqlInterpolated($"DELETE FROM Sources WHERE Id = {id}");
            ctx.SaveChanges();
        }

        public List<int> GetSourceIdsThatHaveIncomes()
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            return ctx.Sources.Include(s => s.Incomes).Where(s => s.Incomes.Count > 0).Select(s => s.Id).ToList();
        }

        public List<IncomeSource> GetAllSources()
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            return ctx.Sources.ToList();
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

        public void AddMaaserPayment(MaaserPayment maaser)
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            ctx.MaaserPayments.Add(maaser);
            ctx.SaveChanges();
        }

        public List<MaaserPayment> GetAllMaaserPayments()
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            return ctx.MaaserPayments.ToList();
        }

        public decimal GetTotalIncome()
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            return ctx.Incomes.Sum(i => i.Amount);
        }
        public decimal GetTotalMaaser()
        {
            var ctx = new MaaserTrackerDbContext(_connectionString);
            return ctx.MaaserPayments.Sum(m => m.Amount);
        }
    }
}
