using System.Collections.Generic;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using WebReport.Model;

namespace WebReport.Bll
{
    public class ExportExcelHandle
    {
        public static void ExportExcel(DataTable dt , string path, string templatePath, List<TtileRow> tieList)
        {
            var templateFile = new FileStream(templatePath, FileMode.Open, FileAccess.Read);//读取模版文件
            var hssfworkbook = new HSSFWorkbook(templateFile);

            ISheet sheet1 = hssfworkbook.GetSheetAt(0);

            ICellStyle style = hssfworkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            style.VerticalAlignment = VerticalAlignment.CENTER;

            foreach (var title in tieList)
            {
                IRow rowTitle = sheet1.GetRow(title.R);
                ICell celltitle = rowTitle.GetCell(title.C);
                if (celltitle.IsMergedCell)
                {
                    //celltitle.
                }
                if(!string.IsNullOrEmpty(title.Description))
                    celltitle.SetCellValue(title.Description + celltitle.StringCellValue);
            }

            var rowIndex = 5;
            //插入表格内容
            foreach (DataRow row in dt.Rows)
            {
                IRow dataRow = sheet1.CreateRow(rowIndex);
                foreach (DataColumn column in dt.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }

            sheet1.ForceFormulaRecalculation = true;

            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
            //file.Position = 0;
            //file.Flush();
        }
    }
}
