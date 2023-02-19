using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp
{
    internal class Helper
    {

        public static void TestGetAllUsers()
        {
            try
            {
                List<UserData> users = SqlConnection.GetAllUsers();

                if (users != null && users.Count > 0)
                {
                    Console.WriteLine($"Retrieved {users.Count} users:");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"User ID: {user.user_id}, Username: {user.username}, Email: {user.user_email}, First Name: {user.first_name}, Last Name: {user.last_name}");
                    }
                }
                else
                {
                    Console.WriteLine("No users found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving users: " + ex.Message);
            }
        }
    }
}
