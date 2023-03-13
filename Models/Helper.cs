using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp.Models
{
    internal class Helper
    {

        public static void TestGetAllUsers()
        {
            try
            {
                List<UserData> users = Data.PostgresDataAccess.GetAllUsers();

                if (users != null && users.Count > 0)
                {
                    Console.WriteLine($"Retrieved {users.Count} users:");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"User ID: {user.id}, Username: {user.username}, Email: {user.email}, First Name: {user.first_name}, Last Name: {user.last_name}");
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
                username = "nachotester",
                email = "nacho@tester.com",
                user_password = "1234",
                first_name = "ignacio",
                last_name = "tester",
                date_of_birth = new DateTime(2000, 1, 1),
                address = "new hardgatan USA",
                phone = "444-1234",
                is_verified = true,
                user_role = 3
            };
            Data.PostgresDataAccess.CreateNewUser(user);
            Console.WriteLine($"User ID: {user.id}, Username: {user.username}, Email: {user.email}, First Name: {user.first_name}, Last Name: {user.last_name}");
        }

        public static void TestUpdateUser()
        {
            UserData user = new UserData()
            {
                id = 3,
                username = "pepetester",
                email = "pepe@tester.com",
                user_password = "1234",
                first_name = "pepe",
                last_name = "tester",
                date_of_birth = new DateTime(2000, 1, 1),
                address = "new hardgatan USA",
                phone = "222-1234",
                is_verified = true,
                user_role = 3
            };
            Data.PostgresDataAccess.UpdateUser(user);
            Console.WriteLine($"User ID: {user.id}, Username: {user.username}, Email: {user.email}, First Name: {user.first_name}, Last Name: {user.last_name}");
            //acomen
        }

        public static void TestGetUserById()
        {
            UserData user = Data.PostgresDataAccess.GetUserById(1);

            Console.WriteLine($"User ID: {user.id}, Username: {user.username}, Email: {user.email}, First Name: {user.first_name}, Last Name: {user.last_name}");
        }

        //
    }
}
