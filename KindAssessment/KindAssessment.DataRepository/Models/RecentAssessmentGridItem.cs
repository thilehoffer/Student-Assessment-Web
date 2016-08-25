using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentApp.Data.Models
{
    public class RecentAssessmentGridItem
    {
        public int Id { get; set; }
        public DateTime Updated { get; set; }
        public string StudentName { get; set; }
        public string Assessment { get; set; }

        public string UpdatedString => Updated.ToString("g");
    }
}
