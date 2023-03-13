using System.Numerics;
using System;
using static System.Console;
using WizzyLiConsoleApp.MenuModule;
using WizzyLiConsoleApp.Models;

namespace WizzyLiConsoleApp
{
    internal class Program
    {
        //CHECKLIST
        //TODO: LOGIN MENU
        //TODO: LOGIN SYSTEM
        //TODO: REGISTRATION SYSTEM
        //TODO: MENU SYSTEM - REFACTOR MENUSYSTEM
        //(TODO: define menu for REGular User Type, admin and manager)
        //TODO: SUBMENU SYSTEM
        //TODO: MENU METHODS
        //TODO: a test
        static void Main(string[] args)
        {
            WelcomeMessageScreen();
            MainMenu();
        }

        static void WelcomeMessageScreen()
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine(@"               ,    _      ");
            WriteLine(@"              /|   | |     ");
            WriteLine(@"            _/_\_  >_<     ");
            WriteLine(@"           .-\-/.   |      ");
            WriteLine(@"          /  | | \_ |      ");
            WriteLine(@"          \ \| |\__(/      ");
            ResetColor();
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine(@"      ____________________ ");
            WriteLine(@"     |                    |");
            WriteLine(@"     |    WELCOME TO      |");
            WriteLine(@"     | WizzyLi BUDGET APP |");
            WriteLine(@"     |____________________|");
            ResetColor();
            WriteLine("\n      Press ENTER to Start");
            ReadLine();
        }
        public static void MainBanner()
        {
            Clear();
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine(@"         ____________________ ");
            WriteLine(@"        |                    |");
            WriteLine(@"        |    WELCOME TO      |");
            WriteLine(@"        | WizzyLi BUDGET APP |");
            WriteLine(@"        |____________________|");
            ResetColor();
        }

        static void MainMenu()
        {
            var menuItems = new List<MenuItem>()
            {
                new MenuItem("Manage Users", ManageUsersMenu),
                new MenuItem("Manage Projects", null),
                new MenuItem("Manage Tasks", null),
                new MenuItem("Manage Categories", null),
                new MenuItem("Manage Expenses", null),
                new MenuItem("Manage Incomes", null),
                new MenuItem("Manage Budgets", null),
                new MenuItem("Manage Reports", null),
            };
            Menu.ManageMenu(menuItems);
        }
        static void ManageUsersMenu()
        {
            var menuItems = new List<MenuItem>();
            var choicesManager = new List<MenuItem>();
            var choicesAdmin = new List<MenuItem>();

            menuItems.Add(new MenuItem("View Users", null));

            choicesManager.Add(new MenuItem("Add User", null));
            choicesManager.Add(new MenuItem("Edit User", null));

            choicesAdmin.Add(new MenuItem("Delete User", null));

            UserData currentUser = new UserData();
            if (currentUser.user_role == (int)UserRole.Manager)
            {
                menuItems.AddRange(choicesManager);
            }

            if (currentUser.user_role == (int)UserRole.Admin)
            {
                menuItems.AddRange(choicesAdmin);
            }
            Menu.ManageMenu(menuItems);
        }

        public enum UserRole
        {
            Admin = 1,
            Manager = 2,
            Regular = 3
        }
        static void GoBackMenuOptions()
        {
            Console.WriteLine("\n\tPress ENTER to go back to the menu.\n");
            Console.ReadLine();
        }
        static void InvalidOption()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\tERROR: Invalid Option!".ToUpper());
            Console.ResetColor();
        }


    }
}