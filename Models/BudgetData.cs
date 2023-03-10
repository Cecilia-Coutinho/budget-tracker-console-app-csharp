using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp.Models
{
    internal class BudgetData
    {
        public int id { get; set; }
        public int project_id { get; set; }
        public int category_id { get; set; }
        public string? budget_name { get; set; }
        public decimal amount { get; set; }
        public DateTime? period_start { get; set; }
        public DateTime? period_end { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
