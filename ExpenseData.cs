using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp
{
    internal class ExpenseData
    {
        public int expense_id { get; set; }
        public int project_id { get; set; }
        public int budget_id { get; set; }
        public string? expense_name { get; set; }
        public string? expense_description { get; set; }
        public decimal expense_amount { get; set; }
        public DateTime? expense_period_start { get; set; }
        public DateTime? expense_period_end { get; set; }
        public DateTime? expense_created_at { get; set; }
        public DateTime? expense_updated_at { get; set; }
    }
}
