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
        public static async Task ImportMasterItemBarCode(IFormFile file)
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

                    #region string builder HTML
                    sb.Append("<table class='table table-bordered'><tr>");
                    List<string> columnNames = new List<string>();
                    foreach (var firstRowCell in worksheet.Cells[worksheet.Dimension.Start.Row, worksheet.Dimension.Start.Column, 1, worksheet.Dimension.End.Column])
                    {
                        columnNames.Add(firstRowCell.Text);
                        sb.Append("<th>" + firstRowCell.Text.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    var start = worksheet.Dimension.Start;
                    var end = worksheet.Dimension.End;
                    for (int i = start.Row + 1; i <= end.Row; i++)
                    { // Row by row...
                        for (int col = start.Column; col <= end.Column; col++)
                        { // ... Cell by cell...
                            object cellValue = worksheet.Cells[i, col].Text; // This got me the actual value I needed.
                            sb.Append("<td>" + cellValue.ToString() + "</td>");
                        }
                        sb.AppendLine("</tr>");
                    }
                    sb.Append("</table>");


                    #endregion

                    for (int row = 2; row <= rowcount; row++)
                    {
                        list.Add(new MobMasterStockModel
                        {
                            BarCode = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            Name = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            Unit = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            Description = worksheet.Cells[row, 4].Value.ToString().Trim()
                        });
                    }

                }
            }
        }
    }
}
