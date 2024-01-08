using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ReactMaaserTrackerMUI_Starter.Data
{
    public class MaaserRepo
    {
        private string _connectionString;
        public MaaserRepo(string connectionString)
        {
            _connectionString = connectionString;
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
