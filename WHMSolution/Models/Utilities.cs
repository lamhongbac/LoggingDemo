using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHMSolution.Models
{
    public class Utilities
    {
        
        WHMApplication _application;
        public Utilities(WHMApplication application)
        {
            _application = application;
        }
        /// <summary>
        /// import stock master data from Excel upload file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public  async Task<bool> ImportMasterItemBarCode(IFormFile file)
        {
            string userID = _application.UserID;
            bool result = false;
            try
            {
                List<MobMasterStockModel> list = new List<MobMasterStockModel>();
                StringBuilder sb = new StringBuilder();
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowcount = worksheet.Dimension.Rows;

                        int NumberRowHeader = 1;
                        int BarCodeRowHeader = 2;
                        int NameRowHeader = 3;
                        int UnitRowHeader = 4;
                        int DescriptionRowHeader = 5;
                        int StartRow = 2;

                        for (int row = StartRow; row <= rowcount; row++)
                        {
                            string number = worksheet.Cells[row, NumberRowHeader].Value.ToString().Trim();
                            string barCode = worksheet.Cells[row, BarCodeRowHeader].Value.ToString().Trim();
                            string name = worksheet.Cells[row, NameRowHeader].Value.ToString().Trim();
                            string unit = worksheet.Cells[row, UnitRowHeader].Value.ToString().Trim();
                            string description = worksheet.Cells[row, DescriptionRowHeader].Value.ToString().Trim();

                            list.Add(new MobMasterStockModel
                            {
                                CreatedBy = userID, //login user
                                CreatedOn = DateTime.Now,
                                DataState = "Posted",
                                GLocation = "",//mobile only
                                HID = "", //mobile only
                                ID = Guid.NewGuid(),
                                ModifiedBy = userID,
                                ModifiedOn = DateTime.Now,
                                Number = number,
                                BarCode = barCode,
                                UserID = userID,
                                Name = name,
                                Unit = unit,
                                Description = description,
                                SyncDate = DateTime.Now

                            }) ;
                        }

                    }
                }
                SQLDataBase database = _application.DataBase;
                int rows =await database.ImportData(list);
                result = rows>0;
            }
            catch(Exception)
            {
                result = false;
                //throw ex;
            }

           
            return result;
        }

        public bool ExportToExcel(List<StockTransData> stockTransList, List<String> headers)
        {
            string fileName = "InvTransDetail" + DateTime.Now.ToString("ddMMyyhhmm");
            //throw new NotImplementedException();
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                sheets.Append(sheet);

                Row headerRow = new Row();

                foreach (string itemheader in headers)
                {


                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(itemheader);
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                int count = stockTransList.Count;

                for (int i = 0; i < count; i++)
                {
                    Row newRow = new Row();
                    StockTransData item = stockTransList[i];
                    //cell barcode
                    Cell cell_barcode = new Cell();
                    cell_barcode.DataType = CellValues.String;
                    cell_barcode.CellValue = new CellValue(item.BarCode.ToString());
                    newRow.AppendChild(cell_barcode);

                    Cell cell_name = new Cell();
                    cell_name.DataType = CellValues.String;
                    cell_name.CellValue = new CellValue(item.Name.ToString());
                    newRow.AppendChild(cell_name);


                    Cell cell_unit = new Cell();
                    cell_unit.DataType = CellValues.String;
                    cell_unit.CellValue = new CellValue(item.Unit.ToString());
                    newRow.AppendChild(cell_unit);

                    Cell cell_quantity = new Cell();
                    cell_quantity.DataType = CellValues.Number;
                    cell_quantity.CellValue = new CellValue(cell_quantity.ToString());
                    newRow.AppendChild(cell_quantity);
                    //...




                    //...
                    sheetData.AppendChild(newRow);
                }


                workbookPart.Workbook.Save();
                return true;
            }
        }
    }
    public class FileViewModel
    {
        public FileViewModel()
        {
           

        }
        public IFormFile FormFile { get; set; }
    }
    public class AppFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
