
namespace WizzyLiConsoleApp.MenuModule
{
    internal class MenuManager
    {
        private Menu RootMenu;

        public MenuManager(Menu rootMenu)
        {
            RootMenu = rootMenu;
        }
        public void RunMenu()
        {
            RootMenu?.RunMenu();
        }
    }
}
