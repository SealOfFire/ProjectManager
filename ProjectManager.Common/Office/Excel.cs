using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.IO;

namespace ProjectManager.Common.Office
{
    public class Excel
    {
        #region open xml

        public static MemoryStream CreateExcelSteam(DataTable table)
        {
            MemoryStream fileStream = new System.IO.MemoryStream();
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileStream, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "samples" };
                sheets.Append(sheet);

                // 表头
                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
                Row row = new Row();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row.Append(ConstructCell(table.Columns[i].ColumnName, CellValues.String));
                }
                sheetData.AppendChild(row);

                // 内容
                foreach (DataRow dr in table.Rows)
                {
                    Row row2 = new Row();
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        row2.Append(ConstructCell(dr[i].ToString(), CellValues.String));
                    }
                    sheetData.AppendChild(row2);
                }

                workbookPart.Workbook.Save();
            }

            return fileStream;
        }

        public static void CreateExcelDoc(string fileName)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Test Sheet" };

                sheets.Append(sheet);

                workbookPart.Workbook.Save();

                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                // Constructing header
                Row row = new Row();
                row.Append(
                    ConstructCell("Id", CellValues.String),
                    ConstructCell("Name", CellValues.String),
                    ConstructCell("Birth Date", CellValues.String),
                    ConstructCell("Salary", CellValues.String));
                // Insert the header row to the Sheet Data
                sheetData.AppendChild(row);

                worksheetPart.Worksheet.Save();

                // 第二页
                WorksheetPart worksheetPart2 = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart2.Worksheet = new Worksheet();
                SheetData sheetData2 = worksheetPart2.Worksheet.AppendChild(new SheetData());
                Sheet sheet2 = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart2), SheetId = 2, Name = "Test Sheet2" };
                sheets.Append(sheet2);

                // 数据
                Row row2 = new Row();
                row2.Append(
                    ConstructCell("bbb", CellValues.String),
                    ConstructCell("xxx", CellValues.String),
                    ConstructCell("ccc Date", CellValues.String),
                    ConstructCell("sss", CellValues.String));
                // Insert the header row to the Sheet Data
                sheetData2.AppendChild(row2);

                worksheetPart.Worksheet.Save();
            }
        }

        private static Cell ConstructCell(string value, CellValues dataType)
        {
            return new Cell()
            {
                CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(value),
                DataType = new EnumValue<CellValues>(dataType)
            };
        }

        #endregion

        #region NPOI

        public static XSSFWorkbook TableToExcelBookForXLSX(DataTable dt)
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook();
            ISheet sheet = xssfworkbook.CreateSheet("Test");

            //表头
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            return xssfworkbook;
        }

        public static XSSFWorkbook TableToExcelBookForXLSX2(DataTable dt)
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook();
            ISheet sheet = xssfworkbook.CreateSheet("Test");

            //表头
            //IRow row = sheet.CreateRow(0);
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    ICell cell = row.CreateCell(i);
            //    cell.SetCellValue(dt.Columns[i].ColumnName);
            //}

            //数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            return xssfworkbook;
        }

        #endregion
    }
}
