using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5.Model
{
    public class VacancyList
    {
        public Int32 rid { get; set; }
        public string jobtitleen { get; set; }
        public string jobtitleae { get; set; }
        public string gradeen { get; set; }
        public string gradeae { get; set; }
        public string targetdate { get; set; }
        public string createddate { get; set; }
        public string modifieddate { get; set; }
        public Int32 openings { get; set; }
        public string jobcode { get; set; }
        public Int32 vacancystatusid { get; set; }
        public string vacancystatusen { get; set; }
        public string vacancystatusae { get; set; }
        public int totalapplicants { get; set; }
        public int totalhired { get; set; }
        public int totalexpat { get; set; }

        public Int32 employmenttypeid { get; set; }
        public string employmenttypeen { get; set; }
        public string employmenttypeae { get; set; }
        public string experience { get; set; }

        public Int32 locationid { get; set; }
        public string locationen { get; set; }
        public string locationae { get; set; }

        public string clientae { get; set; }
        public string clienten { get; set; }
    }
}
