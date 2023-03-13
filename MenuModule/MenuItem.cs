using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizzyLiConsoleApp.MenuModule
{
    internal class MenuItem
    {
        public string? Title { get; private set; }
        public Action? Action { get; private set; }

        public MenuItem(string title, Action? action)
        {
            Title = title;
            Action = action;
        }
    }
}
