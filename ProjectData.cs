using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp
{
    internal class ProjectData
    {
        public int project_id { get; set; }
        public int user_id { get; set; }
        public string? project_name { get; set; }
        public string? project_description { get; set; }
        public DateTime? project_start_date { get; set; }
        public DateTime? project_end_date { get; set; }
        public DateTime? project_created_at { get; set; }
        public DateTime? project_updated_at { get; set; }
    }

}
