using ConsoleApp5.DapperUnitOfWork;
using ConsoleApp5.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5.Repository
{
    internal class JobSeekerRepository
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public JobSeekerRepository()
        {
            _iUnitOfWork = new UnitOfWork();
        }

        public async Task GetResume()
        {
            try
            {
                string sql = "query";
                IEnumerable<HC_ResumeBank> hC_ResumeBanks = await _iUnitOfWork.Connection.QueryAsync<HC_ResumeBank>(sql);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
