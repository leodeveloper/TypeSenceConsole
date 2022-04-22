using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp5.Model
{
    //This model will be update the HC_RESUMEBANK
    public class HC_ResumeBank
    {
        public string english_full_name { get; set; }
        public string arabic_full_name { get; set; }
        public string email { get; set; }
        public string mobile_number { get; set; }
        public string emirate { get; set; }
        public string city { get; set; }
        public string gender { get; set; }
        public string marital_status { get; set; }
        public string special_needs_options { get; set; }
        public string special_notes { get; set; }
        public string Military_service { get; set; }
        public string notes { get; set; }
        public string do_you_have_driving_license { get; set; }
        public string military_service_batch { get; set; }

        public DateTime? Last_Modified_Date { get; set; }


    }

  

    public class Resume_Education
    {
        public long RID { get; set; }
        public string jobSeekerId { get; set; }
        public string gpa { get; set; }
        public string gryear { get; set; }
        public string education_level { get; set; }
        public string edugroup { get; set; }
        public string eduType { get; set; }
        public string eduMajor { get; set; }
        public string University { get; set; }
        public long GroupId { get; set; }
        public string GroupTitle { get; set; }
        public long TypeId { get; set; }
        public string EducationType { get; set; }
        public long MajorId { get; set; }
        public string SpecializationTitle { get; set; }
        public long UniversityId { get; set; }
        public string UniTitle { get; set; }
        public string training { get; set; }
        public string englishlevel { get; set; }
        public string englishcertificate { get; set; }
        public string englishscore { get; set; }
        public string specializedcertificate { get; set; }
    }

    public class Resume_Employer
    {
        public long RID { get; set; } 
        public string jobSeekerId { get; set; }
        public DateTime? StartDate { get; set; }

        public short? FromMonth { get; set; }
        public int? FromYear { get; set; }

        public DateTime? EndDate { get; set; }

        public short? ToMonth { get; set; }
        public int? ToYear { get; set; }

        public long empId { get; set; }

        public string Title { get; set; }

        public long JobTitleId { get; set; }

        public string JobTitle { get; set; }


    }
}
