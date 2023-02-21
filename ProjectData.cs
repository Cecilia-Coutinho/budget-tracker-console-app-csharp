using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp
{
    internal class ProjectData
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string? project_name { get; set; }
        public string? description { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }

}
