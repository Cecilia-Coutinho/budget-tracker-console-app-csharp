using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp
{
    internal class BudgetData
    {
        public int budget_id { get; set; }
        public int project_id { get; set; }
        public int category_id { get; set; }
        public string? budget_name { get; set; }
        public decimal budget_amount { get; set; }
        public DateTime? budget_period_start { get; set; }
        public DateTime? budget_period_end { get; set; }
        public DateTime? budget_created_at { get; set; }
        public DateTime? budget_updated_at { get; set; }
    }
}
