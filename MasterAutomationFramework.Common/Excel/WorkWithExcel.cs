using System;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Table;

namespace MasterAutomationFramework.Common.Excel
{
    public class WorkWithExcel
    {
        FileInfo fileInfo = new FileInfo(@"C:\Basic_Arkusz.xlsx");

        public void ChartMethod1()
        {
            using (var p = new ExcelPackage(fileInfo))
            {
                //Get the Worksheet created in the previous codesample. 
                ExcelWorksheet excelWorksheet = p.Workbook.Worksheets.Add("Example Empty Chart");
                ExcelChart chart = excelWorksheet.Drawings.AddChart("FindingsChart", eChartType.ColumnClustered);
                chart.Title.Text = "Category Chart";
                chart.SetPosition(10, 0, 3, 0);
                chart.SetSize(800, 300);
                var ser1 = (ExcelChartSerie)(chart.Series.Add(excelWorksheet.Cells["H6:H8"],
                excelWorksheet.Cells["G10:G12"]));
                ser1.Header = "Category";
                p.Save();
            }
        }

        public void ChartMethod2()
        {
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                //create a WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Chart Sheet");

                //fill cell data with a loop, note that row and column indexes start at 1
                Random rnd = new Random();
                for (int i = 2; i <= 11; i++)
                {
                    worksheet.Cells[1, i].Value = "Value " + (i - 1);
                    worksheet.Cells[2, i].Value = rnd.Next(5, 25);
                    worksheet.Cells[3, i].Value = rnd.Next(5, 25);
                }
                worksheet.Cells[2, 1].Value = "Age 1";
                worksheet.Cells[3, 1].Value = "Age 2";

                //create a new piechart of type Line
                ExcelLineChart lineChart = worksheet.Drawings.AddChart("lineChart", eChartType.Line) as ExcelLineChart;

                //set the title
                lineChart.Title.Text = "LineChart Example";

                //create the ranges for the chart
                var rangeLabel = worksheet.Cells["B1:K1"];
                var range1 = worksheet.Cells["B2:K2"];
                var range2 = worksheet.Cells["B3:K3"];

                //add the ranges to the chart
                lineChart.Series.Add(range1, rangeLabel);
                lineChart.Series.Add(range2, rangeLabel);

                //set the names of the legend
                lineChart.Series[0].Header = worksheet.Cells["A2"].Value.ToString();
                lineChart.Series[1].Header = worksheet.Cells["A3"].Value.ToString();

                //position of the legend
                lineChart.Legend.Position = eLegendPosition.Right;

                //size of the chart
                lineChart.SetSize(600, 300);

                //add the chart at cell B6
                lineChart.SetPosition(5, 0, 1, 0);
                excelPackage.Save();
            }
        }


        public void GettingStarted_CreateFile()
        {
            //Create a new ExcelPackage
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "Bartosz Gross";
                excelPackage.Workbook.Properties.Title = "Data Driven Testing Excel File";
                excelPackage.Workbook.Properties.Subject = "EPPlus demo export data";
                excelPackage.Workbook.Properties.Created = DateTime.Now;
                excelPackage.Workbook.Properties.SharedDoc = true;

                //Create the WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                //Add some text to cell A1
                worksheet.Cells["A1"].Value = "Some Test Data";
                //You could also use [line, column] notation:
                worksheet.Cells[1, 2].Value = "Numeric Notation B1 Cell";

                //Save your file
                FileInfo fi = new FileInfo(@"C:\Basic_Arkusz.xlsx");
                excelPackage.SaveAs(fi);
            }
        }

        public void GettingStarted_LoadFile()
        {
            //Opening an existing Excel file
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                //Get a WorkSheet by index. Note that EPPlus indexes are base 1, not base 0!
                ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[1];

                try
                {
                    //Get a WorkSheet by name. If the worksheet doesn't exist, throw an exeption
                    ExcelWorksheet namedWorksheet = excelPackage.Workbook.Worksheets["SomeWorksheet"];
                }
                catch (Exception e) { }

                //If you don't know if a worksheet exist    s, you could use LINQ,
                //So it doesn't throw an exception, but return null in case it doesn't find it
                ExcelWorksheet anotherWorksheet =
                    excelPackage.Workbook.Worksheets.FirstOrDefault(x => x.Name == "SomeWorksheet");

                //Get the content from cells A1 and B1 as string, in two different notations
                string valA1 = firstWorksheet.Cells["A1"].Value.ToString();
                string valB1 = firstWorksheet.Cells[1, 2].Value.ToString();

                //Save your file
                excelPackage.Save();
            }
        }

        public void GettingStarted_AppendData()
        {
            //create a new Excel package from the file
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                //create an instance of the the first sheet in the loaded file
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];

                //add some data
                worksheet.Cells[4, 1].Value = "Added data in Cell A4";
                worksheet.Cells[4, 2].Value = "Added data in Cell B4";
                //Add some text to cell A1
                worksheet.Cells["A2"].Value = "Some Test Data";
                //You could also use [line, column] notation:
                worksheet.Cells[1, 3].Value = "Numeric Notation C1 Cell";

                //save the changes
                excelPackage.Save();
            }
        }

        public void CreateTable()
        {
            //create a new Excel package from the file
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                excelPackage.Workbook.Worksheets.Add("Table Sheet");
                excelPackage.Save();
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Table Sheet"];
                worksheet.Cells["A1"].Value = "Column Name A";
                worksheet.Cells["A2"].Value = "1";
                worksheet.Cells["A3"].Value = "2";
                worksheet.Cells["A4"].Value = "2";
                worksheet.Cells["A5"].Value = "3";
                worksheet.Cells["B1"].Value = "Column Name B";
                worksheet.Cells["B2"].Value = "6";
                worksheet.Cells["B3"].Value = "6";
                worksheet.Cells["B4"].Value = "5";
                worksheet.Cells["B5"].Value = "7";

                //Defining the tables parameters
                int firstRow = 1;
                // last row with data
                int lastRow = worksheet.Dimension.End.Row;
                int firstColumn = 1;
                int lastColumn = worksheet.Dimension.End.Column;
                ExcelRange rg = worksheet.Cells[firstRow, firstColumn, lastRow, lastColumn];
                string tableName = "Table1";

                //Ading a table to a Range
                ExcelTable tab = worksheet.Tables.Add(rg, tableName);

                //Formating the table style
                tab.TableStyle = TableStyles.Light8;
                //save the changes
                excelPackage.Save();
            }
        }
    }
}