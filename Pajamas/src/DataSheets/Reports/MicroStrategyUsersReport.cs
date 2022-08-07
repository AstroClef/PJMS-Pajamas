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

        Worksheet? activeWorksheet;

        public MicroStrategyUsersReport(Workbook workbook, Sheets worksheets, DateTime date)
        {
            Workbook = workbook;
            Worksheets = worksheets;
            List<Worksheet> sheets = new();
            Date = date;
            FileName = $"MSTR_ActiveUsers_{Date.ToString("MMddyy")}";
            Generate();
        }

        private void Generate()
        {
            NewSheet();
            CloseOut();
        }

        public void CollectData()
        {

        }

        public void NewSheet()
        {
            activeWorksheet = Worksheets.Add(Worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            activeWorksheet.Name = "User Counts";

            int rowIndexer = 0;
            var dataBodyRange = activeWorksheet.Range["A1"]; // Temporary holding for variable - will be adjusted after second row.

            //First Row
            rowIndexer++;
            var firstRowRange = activeWorksheet.Range[activeWorksheet.Cells[rowIndexer, 1], activeWorksheet.Cells[rowIndexer, 2]];
            firstRowRange.Interior.Color = XlRgbColor.rgbWheat;
            firstRowRange.Borders.LineStyle = XlLineStyle.xlContinuous;
            firstRowRange.Borders.Weight = XlBorderWeight.xlThin;
            firstRowRange[1].Value = "Date";
            firstRowRange[2].Value = Date.ToString("MM/dd/yyyy");

            //Second Row
            rowIndexer++;
            var secondRowRange = activeWorksheet.Range[activeWorksheet.Cells[rowIndexer, 1], activeWorksheet.Cells[rowIndexer, 5]];
            secondRowRange.Font.Bold = true;
            secondRowRange.Interior.Color = XlRgbColor.rgbYellow;
            secondRowRange[1].Value = "Environment";
            secondRowRange[2].Value = "Active Count";
            secondRowRange[3].Value = "Inactive Count";
            secondRowRange[4].Value = "Totals";
            secondRowRange[5].Value = "Exceptions*";

            //Rows for Each Environment (Page in original document)
            for (int i = 1; i < Worksheets.Count; i++)
            {
                rowIndexer++;
                activeWorksheet.Cells[rowIndexer, 1].Value = Worksheets[i+1].Name;
            }

            //Last Row
            rowIndexer++;
            var lastRowRange = activeWorksheet.Range[activeWorksheet.Cells[rowIndexer, 1], activeWorksheet.Cells[rowIndexer, 5]];
            lastRowRange[1].Value = "*Terminated employees that need access removed.";
            lastRowRange.Merge();

            //Boders for Data Body and Last Row
            dataBodyRange = activeWorksheet.Range[activeWorksheet.Cells[2, 1], activeWorksheet.Cells[rowIndexer, 5]];
            dataBodyRange.Borders.LineStyle = XlLineStyle.xlContinuous;
            dataBodyRange.Borders.Weight = XlBorderWeight.xlThin;

            //Autofit all columns to data
            activeWorksheet.Columns.AutoFit();
        }

        private void CloseOut()
        {
            activeWorksheet = (Worksheet)Workbook.Worksheets[1];
            activeWorksheet.Select();
            Workbook.SaveAs($"{DirManager.GetDirectoryPath("Export")}/{FileName}");
            Workbook.Close();
        }
    }
}
