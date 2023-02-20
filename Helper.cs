using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static void TestCreatingNewUser()
        {
            UserData user = new UserData()
            {
                username = "tobiastester",
                user_email = "tobias@tester.com",
                user_password = "1234",
                first_name = "tobias",
                last_name = "tester",
                date_of_birth = new DateTime(2000, 1, 1),
                user_address = "new hardgatan USA",
                user_phone = "111-1234",
                is_verified = true,
                user_role = 1
            };
            SqlConnection.CreateNewUser(user);
            Console.WriteLine($"User ID: {user.user_id}, Username: {user.username}, Email: {user.user_email}, First Name: {user.first_name}, Last Name: {user.last_name}");
        }

        public static void TestUpdateUser()
        {
            UserData user = new UserData()
            {
                user_id = 3,
                username = "pepetester",
                user_email = "pepe@tester.com",
                user_password = "1234",
                first_name = "pepe",
                last_name = "tester",
                date_of_birth = new DateTime(2000, 1, 1),
                user_address = "new hardgatan USA",
                user_phone = "222-1234",
                is_verified = true,
                user_role = 3
            };
            SqlConnection.UpdateUser(user);
            Console.WriteLine($"User ID: {user.user_id}, Username: {user.username}, Email: {user.user_email}, First Name: {user.first_name}, Last Name: {user.last_name}");
            //acomen
        }

        //
    }
}
