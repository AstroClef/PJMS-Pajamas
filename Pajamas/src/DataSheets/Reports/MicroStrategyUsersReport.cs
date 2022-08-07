using ActiveInactiveUsersReport.src.DirManagment;
using Microsoft.Office.Interop.Excel;

namespace ActiveInactiveUsersReport.src.DataSheets.Reports
{
    public class MicroStrategyUsersReport
    {
        Workbook Workbook { get; set; }
        Sheets Worksheets { get; set; }
        private readonly DateTime Date;
        private readonly string FileName;

        public MicroStrategyUsersReport(Workbook workbook, Sheets worksheets, DateTime date)
        {
            Workbook = workbook;
            Worksheets = worksheets;
            Date = date;
            FileName = $"MSTR_ActiveUsers_{Date.ToString("MMddyy")}";
        }

        private void Generate()
        {
            var newSheet = (Worksheet)Worksheets.Add(Worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            newSheet.Name = "User Counts";
            newSheet.Range["A1"].Value = "new worksheet";

            newSheet = (Worksheet)Workbook.Worksheets[1];
            newSheet.Select();
            CloseOut();
        }

        private void CloseOut()
        {
            Workbook.SaveAs($"{DirManager.GetDirectoryPath("Export")}/{FileName}");
            Workbook.Close();
        }
    }
}
