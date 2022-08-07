using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveInactiveUsersReport.src.DataSheets.Reports
{
    internal class ReportsManager
    {
        private static DateTime date = DateTime.Today;
        public static void GenerateReport(string path, EReports reportType)
        {
            ExcelWorkbook excelworkbook = new ExcelWorkbook(path);
            excelworkbook.Open();
            switch (reportType)
            {
                case EReports.MSTR:
                    MicroStrategyUsersReport mstrReport = new(excelworkbook.GetWorkbook()!, excelworkbook.GetWorkbook()!.Sheets, date);
                    break;
            }
            excelworkbook.Close();
        }
    }
}
