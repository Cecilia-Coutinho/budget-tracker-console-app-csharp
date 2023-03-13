using System.Data;

namespace WizzyLiConsoleApp.MenuModule
{
    internal class Menu
    {
        private List<MenuItem> Items;
        public Menu(List<MenuItem> items)
        {
            Items = items;
        }

        private void PrintMenu(int menuIndex)
        {
            Console.Clear();
            Program.MainBanner();
            string header = $"\n\tUse arrow keys to navigate and press Enter to select." +
                $"\n\tPlease select one of the following options:";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(header);
            Console.ResetColor();

            for (int i = 0; i < Items?.Count; i++)
            {
                if (i == menuIndex - 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                string? menuItem = Items[i].Title;
                Console.Write($"\n\t{i + 1}. {menuItem}");
                Console.ResetColor();
            }
        }

        private int SelectMenuItemWithArrows(ConsoleKeyInfo keyInfo, int currentSelectionIndex)
        {
            int minIndexToSelect = 1;
            int maxIndexToSelect = Items?.Count ?? 0;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (currentSelectionIndex > minIndexToSelect)
                    {
                        currentSelectionIndex--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (currentSelectionIndex < maxIndexToSelect)
                    {
                        currentSelectionIndex++;
                    }
                    break;
            }
            return currentSelectionIndex;
        }

        public void RunMenu()
        {
            bool runMenu = true;
            ConsoleKeyInfo keyInfo;
            int menuIndex = 1;

            while (runMenu)
            {
                Console.Clear();
                PrintMenu(menuIndex);
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    MenuItem selectedMenuItem = Items[menuIndex - 1];
                    // Invoke the corresponding method
                    Console.Clear();
                    selectedMenuItem.Action?.Invoke();
                    Console.WriteLine("\n\tPress any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    menuIndex = SelectMenuItemWithArrows(keyInfo, menuIndex);
                }
            }
        }
        public static void ManageMenu(List<MenuItem> menuItems)
        {
            // Check if "Exit" option has already been added
            if (!menuItems.Any(item => item.Title == "Exit"))
            {
                menuItems.Add(new MenuItem("Exit", null));
            }
            var menuList = new Menu(menuItems);
            new MenuManager(menuList).RunMenu();
        }
    }
}
