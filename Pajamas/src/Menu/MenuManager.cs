using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveInactiveUsersReport.src.Menu
{
    public class MenuManager
    {
        private static Menu activeMenu = new(EMenus.MAIN);
        private static Menu previousMenu = activeMenu;

        public static void SetActiveMenu(EMenus selectedMenu)
        {
            previousMenu = activeMenu;
            activeMenu = new Menu(selectedMenu);
            FullLoadMenu();
        }
        public static void SetActiveMenu(Menu selectedMenu)
        {
            previousMenu = activeMenu;
            activeMenu = selectedMenu;
            FullLoadMenu();
        }

        public static void FullLoadMenu()
        {
            activeMenu.Open();
            activeMenu.Select();
        }

        public static void ReloadMenu()
        {
            activeMenu.Open();
        }

        public static void MenuItemSelect()
        {
            activeMenu.Select();
        }

        public static Menu GetPreviousMenu()
        {
            return previousMenu;
        }
    }
}
