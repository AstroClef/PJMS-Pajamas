using ActiveInactiveUsersReport.src.DataSheets.Reports;
using ActiveInactiveUsersReport.src.DirManagment;

namespace ActiveInactiveUsersReport.src.Menu
{
    public class Menu
    {
        private string title = "";
        private int itemIndex;
        private EMenus activeMenu;
        int selection;
        private List<string> activeItems = new();
        public Menu(EMenus desiredMenu)
        {
            activeMenu = desiredMenu;
            switch (desiredMenu)
            {
                case EMenus.MAIN:
                    MainMenuInit();
                    break;
                case EMenus.FILE_SELECT:
                    FileSelectInit();
                    break;
            }
        }

        public void Open()
        {
            itemIndex = 0;
            Console.Clear();
            Console.WriteLine($"{title}\n");
            Console.WriteLine("Select an option listed below:");
            foreach (string item in activeItems)
            {
                itemIndex++;
                Console.WriteLine($"{itemIndex}. {item}");
            }
        }

        public void Select()
        {
            bool matchesSelectionOption = false;
            while (!matchesSelectionOption)
            {
                selection = Input.ConsoleInputManager.GetValidatedInput<int>();
                if (selection > activeItems.Count || selection <= 0)
                {
                    Input.ConsoleInputManager.PrintError("Input must match a number next to the items listed in the menu.");
                    Open();
                    matchesSelectionOption = false;
                }
                else
                {
                    matchesSelectionOption = true;
                }
            }
            switch (activeMenu)
            {
                case EMenus.MAIN:
                    MainMenuSelection();
                    break;
                case EMenus.FILE_SELECT:
                    FileSelectSelection();
                    break;
            }
        }

        private void MainMenuInit()
        {
            title = "Main Menu";
            activeItems.Clear();
            activeItems.Add("Generate MSTR Report");
            activeItems.Add("Clear Folders");
        }

        private void MainMenuSelection()
        {
            switch (selection)
            {
                case 1:
                    MenuManager.SetActiveMenu(EMenus.FILE_SELECT);
                    break;
                case 2:
                    DirManager.EmptyDirectory();
                    break;
            }
        }

        private void FileSelectInit()
        {
            title = "File Select";
            activeItems.Clear();
            activeItems = DirManager.RecieveFiles();
            activeItems.Add("Go Back");
        }

        private void FileSelectSelection()
        {
            if (selection != activeItems.Count)
            {
                ReportsManager.GenerateReport(activeItems[selection-1], EReports.MSTR);
            } else
            {
                MenuManager.SetActiveMenu(MenuManager.GetPreviousMenu());
            }
        }
    }
}
