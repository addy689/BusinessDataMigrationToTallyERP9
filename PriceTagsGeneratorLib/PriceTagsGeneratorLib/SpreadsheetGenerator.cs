using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;

using OfficeOpenXml;
using OfficeOpenXml.Style;
using PriceTagsGeneratorLib.Models;

namespace PriceTagsGeneratorLib
{
    public class SpreadsheetGenerator
    {
        public CodesCSVItemModel[][] ConstructNonFormattedSpreadsheetData(List<CodesCSVItemModel> itemsList, int colsInPage)
        {
            int colIndex = -1;
            int itemCtr = 0;

            List<CodesCSVItemModel[]> rows = new List<CodesCSVItemModel[]>();

            List<CodesCSVItemModel> currentRow = null;
            foreach (var item in itemsList)
            {
                if (colIndex == colsInPage || colIndex == -1)
                {
                    if (currentRow != null)
                        rows.Add(currentRow.ToArray());

                    colIndex = 0;
                    currentRow = new List<CodesCSVItemModel>();
                }

                currentRow.Add(item);
                colIndex++;
                itemCtr++;

                if (itemCtr == itemsList.Count)
                    rows.Add(currentRow.ToArray());
            }

            return rows.ToArray();
        }

        public void ConvertToRichlyFormattedSpreadsheet(CodesCSVItemModel[][] dataRows, string outputFile, IConfigReader configReader)
        {
            FileInfo newFile = new FileInfo(outputFile);

            using (ExcelPackage pck = new ExcelPackage(newFile))
            {

                var worksheet = pck.Workbook.Worksheets.Add("PrintableTags");

                //Fetch configuration values
                var maxColumnsToUse = int.Parse(configReader.GetConfiguration("MaximumSpreadsheetColumnsToUse"));
                var colWidth = double.Parse(configReader.GetConfiguration("SpreadsheetColumnWidth"));
                var rowHeight = double.Parse(configReader.GetConfiguration("SpreadsheetRowHeight"));
                var priceFontSize = float.Parse(configReader.GetConfiguration("PriceFontSizeOnTag"));
                var codeFontSize = float.Parse(configReader.GetConfiguration("CodeFontSizeOnTag"));

                //Set required column widths for the worksheet
                worksheet.Column(1).BestFit = false;
                worksheet.Column(1).Width = colWidth;
                worksheet.Column(1).ColumnMax = 18; //column upto which the style (col width, etc) applies

                //Creating style for price+code cells
                var pricetagCellStyle = worksheet.Workbook.Styles.CreateNamedStyle("PriceTagCellStyle");
                pricetagCellStyle.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                pricetagCellStyle.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                pricetagCellStyle.Style.WrapText = true; //required for '\n' to work in cell text

                Border b = pricetagCellStyle.Style.Border;
                b.Top.Style = b.Bottom.Style = b.Left.Style = b.Right.Style = ExcelBorderStyle.Thin;
                
                //Fill the spreadsheet with data
                for (var rowIndex = 1; rowIndex <= dataRows.Length; rowIndex++)
                {
                    var dataRow = dataRows[rowIndex - 1];
                    worksheet.Row(rowIndex).CustomHeight = true;
                    worksheet.Row(rowIndex).Height = rowHeight;

                    for (var colIndex = 1; colIndex <= dataRow.Length; colIndex++)
                    {
                        var item = dataRow[colIndex - 1];
                        worksheet.Cells[rowIndex, colIndex].StyleName = "PriceTagCellStyle";

                        //Add price and code as rich text to the cell
                        var rt = worksheet.Cells[rowIndex, colIndex].RichText;

                        var r1 = rt.Add("$" + item.Price);
                        r1.Size = priceFontSize;
                        r1.Bold = true;
                        
                        var r2 = rt.Add("\n" + item.Code);
                        r2.Size = codeFontSize;
                        r2.Bold = false;
                    }
                }

                pck.Save();
            }

        }

        //workbook = xlsxwriter.Workbook(outputFile)
        //worksheet = workbook.add_worksheet()

        //#set all the formats
        //priceFont = workbook.add_format({'size' : config.PriceFontSizeOnTag})
        //priceFont.set_bold()
        //codeFont = workbook.add_format({'size' : config.CodeFontSizeOnTag})

        //for rowIndex in range(0, len(data)):
        //    row = data[rowIndex]
        //    worksheet.set_row(rowIndex, config.SpreadsheetRowHeight)
        //    for colIndex in range(0, len(row)):
        //        item = row[colIndex]
        //        worksheet.write_rich_string(rowIndex, colIndex, priceFont, '$' \
        //        + str(item.price) + '\n', codeFont, str(item.code), cellFormat)

        //workbook.close()

    }
}
