using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp
{
    internal class UserData
    {
        public int user_id { get; set; }
        public string? username { get; set; }
        public string? user_email { get; set; }
        public string? user_password { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public string? user_address { get; set; }
        public string? user_phone { get; set; }
        public bool is_verified { get; set; }
        public DateTime user_created_at { get; set; }
        public DateTime? user_updated_at { get; set; }
        public int user_role { get; set; }
    }


}
