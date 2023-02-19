using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp
{
    internal class CategoryData
    {
        public int category_id { get; set; }
        public string? category_name { get; set; }
        public string? category_description { get; set; }
        public DateTime? category_created_at { get; set; }
        public DateTime? category_updated_at { get; set; }
    }
}
