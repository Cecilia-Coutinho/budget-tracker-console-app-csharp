using System.Numerics;
using System;
using static System.Console;

namespace WizzyLiConsoleApp
{
    internal class Program
    {
        //CHECKLIST
        //TODO: LOGIN MENU
        //TODO: LOGIN SYSTEM
        //TODO: REGISTRATION SYSTEM
        //TODO: MENU SYSTEM - SKELETON DONE
        //(TODO: define menu for REGular User Type, admin and manager)
        //TODO: SUBMENU SYSTEM
        //TODO: MENU METHODS
        //TODO
        //TODO
        static void Main(string[] args)
        {
            //Helper.TestGetAllUsers();
            WelcomeMessageScreen();
            RunMenu();
            //Helper.TestCreatingNewUser();
            //Helper.TestGetUserById();
        }

        static void WelcomeMessageScreen()
        {
            //Clear();
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

        static void PrintCenteredText(string text)
        {
            int width = WindowWidth;
            int padding = (width - text.Length) / 2;
            string centeredText = text.PadLeft(padding + text.Length).PadRight(width);
            WriteLine(centeredText);
        }
        static int MenuChoiceArrowsSystem(ConsoleKeyInfo keyInfo, int menuChoice, List<string> menuOptions)
        {
            int minMenuChoice = 1;
            int maxMenuChoice = menuOptions.Count;
            if (keyInfo.Key == ConsoleKey.UpArrow && menuChoice > minMenuChoice)
            {
                menuChoice--;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow && menuChoice < maxMenuChoice)
            {
                menuChoice++;
            }
            return menuChoice;
        }

        static void DisplayMenuOptions(List<string> menuOptions, int menuChoice)
        {
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine(@"         ____________________ ");
            WriteLine(@"        |                    |");
            WriteLine(@"        |    WELCOME TO      |");
            WriteLine(@"        | WizzyLi BUDGET APP |");
            WriteLine(@"        |____________________|");
            ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n\tUse arrow keys to navigate and press Enter to select.");
            Console.WriteLine("\n\tPlease select one of the following options:\n");
            Console.ResetColor();

            int maxMenuChoice = menuOptions.Count - 1;
            for (int i = 0; i <= maxMenuChoice; i++)
            {
                if (i == menuChoice - 1)
                {
                    ForegroundColor = ConsoleColor.DarkYellow;
                }
                Console.Write($"\n\t {i + 1}. {menuOptions[i]}");
                ResetColor();
            }
        }
        static void RunMenu()
        {
            bool runMenu = true;
            ConsoleKeyInfo keyInfo;
            int menuChoice = 1;
            List<string> regularMainMenu = new List<string>()
            {
                "Manage Users\n",
                "Manage Projects\n",
                "Manage Tasks\n",
                "Manage Categories\n",
                "Manage Expenses\n",
                "Manage Incomes\n",
                "Manage Budgets\n",
                "Manage Reports\n",
                "Exit\n"
            };

            while (runMenu)
            {
                Console.Clear();
                DisplayMenuOptions(regularMainMenu, menuChoice);
                keyInfo = Console.ReadKey(true);
                menuChoice = MenuChoiceArrowsSystem(keyInfo, menuChoice, regularMainMenu);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    switch (menuChoice)
                    {
                        case 1:
                            //1. Manage Users
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("coming soon");
                            Console.ResetColor();
                            GoBackMenuOptions();
                            break;
                        case 2:
                            //2. Manage Projects
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("coming soon");
                            Console.ResetColor();
                            GoBackMenuOptions();
                            break;
                        case 3:
                            //3. Manage Tasks
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("coming soon");
                            Console.ResetColor();
                            GoBackMenuOptions();
                            break;
                        case 4:
                            //4. Manage Categories
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("coming soon");
                            Console.ResetColor();
                            GoBackMenuOptions();
                            break;
                        case 5:
                            //5. Manage Expenses
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("coming soon");
                            Console.ResetColor();
                            GoBackMenuOptions();
                            break;
                        case 6:
                            //6. Manage Incomes
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("coming soon");
                            Console.ResetColor();
                            GoBackMenuOptions();
                            break;
                        case 7:
                            //7. Manage Budgets
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("coming soon");
                            Console.ResetColor();
                            GoBackMenuOptions();
                            break;
                        case 8:
                            //8. Manage Reports
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("coming soon");
                            Console.ResetColor();
                            GoBackMenuOptions();
                            break;
                        case 9:
                            //0. Exit
                            Console.Clear();
                            Console.WriteLine("\n\tThank you for using WizzyLi. We look forward to your next visit!");
                            Thread.Sleep(1000);
                            Environment.Exit(0);
                            break;
                            //default:
                            //    Console.Clear();
                            //    InvalidOption();
                            //    Console.ForegroundColor = ConsoleColor.Red;
                            //    Console.WriteLine("\n\tTo navigate between menu options, use your arrow keys and select an option\n");
                            //    Console.ResetColor();
                            //    GoBackMenuOptions();
                            //    break;
                    }
                }
            }
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