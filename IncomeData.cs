using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp
{
    internal class IncomeData
    {
        public int income_id { get; set; }
        public int project_id { get; set; }
        public int budget_id { get; set; }
        public string? income_name { get; set; }
        public string? income_description { get; set; }
        public decimal income_amount { get; set; }
        public DateTime? income_period_start { get; set; }
        public DateTime? income_period_end { get; set; }
        public DateTime? income_created_at { get; set; }
        public DateTime? income_updated_at { get; set; }
    }
}
