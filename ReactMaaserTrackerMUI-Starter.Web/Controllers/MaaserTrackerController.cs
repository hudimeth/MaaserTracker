using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactMaaserTrackerMUI_Starter.Data;
using ReactMaaserTrackerMUI_Starter.Web.ViewModels;
using System.Diagnostics.Tracing;
using System.Security.Cryptography.X509Certificates;

namespace ReactMaaserTrackerMUI_Starter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaaserTrackerController : ControllerBase
    {
        private string _connectionString;
        public MaaserTrackerController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("getallsources")]
        public List<IncomeSource> GetAllSources()
        {
            var repo = new MaaserTrackerRepo(_connectionString);
            return repo.GetAllSources();
        }

        [HttpPost]
        [Route("deletesource")]
        public void DeleteSource(DeleteSourceViewModel vm)
        {
            var repo = new MaaserTrackerRepo(_connectionString);
            repo.DeleteSource(vm.SourceId);
        }


        [HttpPost]
        [Route("addsource")]
        public void AddSource(AddSourceViewModel viewModel)
        {
            var repo = new MaaserTrackerRepo(_connectionString);
            repo.AddSource(viewModel.SourceName);
        }

        [HttpGet]
        [Route("getsourceidsthathaveincomes")]
        public List<int> GetSourceIdsThatHaveIncomes()
        {
            var repo = new MaaserTrackerRepo(_connectionString);
            return repo.GetSourceIdsThatHaveIncomes();
        }


        [HttpPost]
        [Route("editsource")]
        public void EditSource(EditSourceViewModel viewModel)
        {
            var repo = new MaaserTrackerRepo(_connectionString);
            repo.EditSource(viewModel.Id, viewModel.SourceNameEdited);
        }

        [HttpPost]
        [Route("addmaaserpayment")]
        public void AddMaaserPayment(MaaserPayment maaser)
        {
            var repo = new MaaserTrackerRepo(_connectionString);
            repo.AddMaaserPayment(maaser);
        }

        [HttpGet]
        [Route("getallmaaserpayments")]
        public List<MaaserPayment> GetAllMaaserPayments()
        {
            var repo = new MaaserTrackerRepo(_connectionString);
            return repo.GetAllMaaserPayments();
        }

        [HttpGet]
        [Route("getallincomes")]
        public List<IncomesViewModel> GetAllIncomes()
        {
            var repo = new MaaserTrackerRepo(_connectionString);
            var incomes = repo.GetAllIncomes();
            return incomes.Select(i => new IncomesViewModel
            {
                SourceName = repo.GetSourceName(i.SourceId),
                Amount = i.Amount,
                Date = i.Date
            }).ToList();
        }
        [HttpPost]
        [Route("addincome")]
        public void AddIncome(Income income)
        {
            var repo = new MaaserTrackerRepo(_connectionString);
            repo.AddIncome(income);
        }

        [HttpGet]
        [Route("getgroupedincomes")]
        public List<GroupedIncomesViewModel> SourcesWithIncomes()
        {
            var repo = new MaaserTrackerRepo(_connectionString);
            var sourcesWithIncomes = repo.GetSourcesWithIncomes();

            //didn't work cuz invalid casting:
            //var sourceNamesWithIncomes = (List<GroupedIncomesViewModel>)sourcesWithIncomes.Select(s => new GroupedIncomesViewModel
            //{
            //    SourceName = s.Source,
            //    Incomes = (List<IncomesViewModel>)s.Incomes.Select(i => new IncomesViewModel
            //    {
            //        SourceName = s.Source,
            //        Amount = i.Amount,
            //        Date = i.Date
            //    }).ToList()
            //}) ;
            var sourceNamesWithIncomes = new List<GroupedIncomesViewModel>();
            sourcesWithIncomes.ForEach(s =>
            {
                List<IncomesViewModel> incomesVM = new List<IncomesViewModel>();
                if (s.Incomes != null)
                {
                    foreach (var income in s.Incomes)
                    {
                        incomesVM.Add(new IncomesViewModel
                        {
                            SourceName = s.Source,
                            Amount = income.Amount,
                            Date = income.Date
                        });
                    }
                }
                sourceNamesWithIncomes.Add(new GroupedIncomesViewModel
                {
                    SourceName = s.Source,
                    Incomes = incomesVM
                });
            });

            return sourceNamesWithIncomes;
        }

        [HttpGet]
        [Route("getmaaseroverview")]
        public MaaserOverviewViewModel GetMaaserOverview()
        {
            var repo = new MaaserTrackerRepo(_connectionString);
            var totalIncome = repo.GetTotalIncome();
            var totalMaaser = repo.GetTotalMaaser();
            var maaserObligated = totalIncome / 10;

            return new()
            {
                TotalIncome = totalIncome,
                TotalMaaserGiven = totalMaaser,
                MaaserObligated = maaserObligated,
                RemainingMaaserObligation = maaserObligated - totalMaaser
            };
        }
    }
}
