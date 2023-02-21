using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp
{
    internal class ExpenseData
    {
        public int id { get; set; }
        public int project_id { get; set; }
        public int budget_id { get; set; }
        public string? expense_name { get; set; }
        public string? description { get; set; }
        public decimal amount { get; set; }
        public DateTime? period_start { get; set; }
        public DateTime? period_end { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
