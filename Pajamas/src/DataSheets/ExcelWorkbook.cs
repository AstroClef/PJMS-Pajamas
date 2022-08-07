using Microsoft.Office.Interop.Excel;

namespace ActiveInactiveUsersReport.src.DataSheets
{
    class ExcelWorkbook
    {
        string Path { get; set; }
        private Application? xl_application;
        private Workbook? workbook;

        public ExcelWorkbook(string path)
        {
            Path = path;
        }

        public void Open()
        {
            xl_application = new Application{ DisplayAlerts = false };
            workbook = xl_application.Workbooks.Open(Path);
        }

        public void Close()
        {
            try
            {
                xl_application?.Quit();
            } catch (Exception ex)
            {
                Input.ConsoleInputManager.PrintError($"{ex.ToString}\n\n No Excel Application has been started.");
            }
        }

        public Workbook? GetWorkbook()
        {
            return workbook;
        }
    }
}
