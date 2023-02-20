using System.Numerics;
using System;
using static System.Console;

namespace WizzyLiConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Helper.TestGetAllUsers();
            WelcomeMessageScreen();
            //Helper.TestCreatingNewUser();
        }

        //Welcome to the Application!

        //Please select an option:
        //1. Manage Users
        //2. Manage Projects
        //3. Manage Tasks
        //4. Manage Categories
        //5. Manage Expenses
        //6. Manage Incomes
        //7. Manage Budgets
        //8. Manage Reports
        //0. Exit

        //Your choice: 

        static void WelcomeMessageScreen()
        {
            //Clear();
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine(@"               ,    _      ");
            WriteLine(@"              /|   | |     ");
            WriteLine(@"            _/_\_  >_<     ");
            WriteLine(@"           .-\-/.   |      ");
            WriteLine(@"          /  | | \_ |      ");
            WriteLine(@"          \ \| |\__(/      ");
            ResetColor();
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine(@"      ____________________ ");
            WriteLine(@"     |                    |");
            WriteLine(@"     |    WELCOME TO      |");
            WriteLine(@"     | WizzyLi BUDGET APP |");
            WriteLine(@"     |____________________|");
            ResetColor();
            WriteLine("\n      Press ENTER to Start");
            ReadLine();
        }

        static void PrintCenteredText(string text)
        {
            int width = WindowWidth;
            int padding = (width - text.Length) / 2;
            string centeredText = text.PadLeft(padding + text.Length).PadRight(width);
            WriteLine(centeredText);
        }

        //TODO: LOGIN MENU
        //TODO: LOGIN SYSTEM
        //TODO: REGISTRATION SYSTEM


    }
}