using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactMaaserTrackerMUI_Starter.Data
{
    public class SourceRepo
    {
        private string _connectionString;
        public SourceRepo(string connectionString)
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
    }
}
