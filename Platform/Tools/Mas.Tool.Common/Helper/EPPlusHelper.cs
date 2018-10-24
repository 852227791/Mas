using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Ax.Core.Common.Helper
{
    public class EPPlusHelper
    {
        public static string DownloadExcel<T>(List<T> list)
        {
            var fileName = FileName(".xlsx");
            var file = new FileInfo(fileName);
            using (var package = new ExcelPackage(new FileStream(fileName, FileMode.Open)))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                List<int> delCol = new List<int>();
                int colNum = 1;
                worksheet.Cells.AutoFitColumns();
                foreach (var item in list)
                {
                    var i = 1;
                    foreach (var p in item.GetType().GetProperties())
                    {
                        var desObj = p.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        var des = new DescriptionAttribute();
                        if (desObj.Length > 0)
                        {
                            des = desObj[0] as DescriptionAttribute;
                            if (des.Description == "不显示")
                            {
                                delCol.Add(colNum);
                            }
                            worksheet.Cells[1, i].Value = des.Description;
                        }
                        else
                        {
                            worksheet.Cells[1, i].Value = p.Name;
                        }
                        colNum++;
                    }
                    break;
                }
                // 添加worksheet
                worksheet.Cells.LoadFromCollection(list, true);
                if (delCol.Count > 0)
                {
                    delCol.ForEach(m =>
                    {
                        worksheet.DeleteColumn(m);
                    });
                }
                package.Save();
                package.Dispose();
            }
            var address = "http://{0}/" + fileName.Replace("wwwroot/", string.Empty);
            return address;
        }


        public static string DownloadExcel(DataTable dt)
        {
            var fileName = FileName(".xlsx");
            var file = new FileInfo(fileName);
            using (var package = new ExcelPackage(new FileStream(fileName, FileMode.Open)))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells.AutoFitColumns();
                // 添加worksheet
                worksheet.Cells.LoadFromDataTable(dt, true);
                package.Save();
                package.Dispose();
            }
            var address = "http://{0}/" + fileName.Replace("wwwroot/", string.Empty);
            return address;
        }

        public static string DownloadExcelByGrid(string Html)
        {
            var fileName = FileName(".xls");

            File.WriteAllText(fileName, Html, Encoding.UTF8);

            var address = "http://{0}/" + fileName.Replace("wwwroot/", string.Empty);
            return address;
        }

        public static string FileName(string suffix)
        {
            var R = new Random(); //创建产生随机数
            var val = 100 + R.Next(899); //产生随机数为99以内任意
            var sj = val.ToString(); //产生随机数
            var FileTime = DateTime.Now.ToString("yyyyMMddHHmmss") + sj; //得到系统时间(格式化)并加上随机数以便生成上传图片名称
            //产生上传图片的名称
            var fileload = DateTime.Now.ToString("yyyyMM"); //每月产生一个文件夹名
            var fileloaddate = DateTime.Now.ToString("dd"); //每天一个文件夹
            var year = DateTime.Now.ToString("yyyy");
            var month = DateTime.Now.ToString("MM");
            var day = DateTime.Now.ToString("dd");
            var filename = "wwwroot/DownloadFile/" + year + "/" + month + "/" + day + "/" + FileTime + suffix;

            var pos = filename.LastIndexOf("/");
            var files = filename.Substring(0, pos);
            if (!Directory.Exists(files))
                Directory.CreateDirectory(files);
            if (!File.Exists(filename))
                File.Create(filename).Dispose();
            return filename;
        }

        /// <summary>
        ///     将导入的数据转换为DataTable
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExportToDataTable(FileInfo file)
        {
            var dt = new DataTable();
            using (var package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets[1];
                var rowCount = worksheet.Dimension.Rows;
                var ColCount = worksheet.Dimension.Columns;
                for (var col = 1; col <= ColCount; col++)
                    dt.Columns.Add(worksheet.Cells[1, col].Value?.ToString());
                for (var row = 2; row <= rowCount; row++)
                {
                    var dataRow = dt.NewRow();
                    for (var col = 1; col <= ColCount; col++)
                    {
                        var temp = worksheet.Cells[row, col].Value;
                        dataRow[col - 1] = temp == null ? string.Empty : temp.ToString();
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }


        /// <summary>
        ///     通过Cell名称获取String值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetDrString(DataRow dr, string name)
        {
            try
            {
                return dr[name]?.ToString().Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        ///     excel读取 返回符合前端datagrid 的json
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static string ExcelToJsonString(string filePath, string columns)
        {
            var colList = JsonConvert.DeserializeObject<List<DataGridColumns>>(columns);
            var sb = new StringBuilder();
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            using (var package = new ExcelPackage())
            {
                package.Load(fs);
                var sheet = package.Workbook.Worksheets[1];
                if (sheet.Dimension == null)
                    return "[]";
                var columnCount = sheet.Dimension.End.Column;
                var rowCount = sheet.Dimension.End.Row;
                if (rowCount > 0)
                {
                    sb.Append("[");
                    for (var i = 2; i <= rowCount; i++)
                    {
                        var tempsb = new StringBuilder();
                        tempsb.Append("{");
                        for (var col = 1; col <= columnCount; col++)
                        {
                            var titleName = sheet.Cells[1, col].Value == null
                                ? string.Empty
                                : sheet.Cells[1, col].Value.ToString();
                            var colModel = colList.Where(m => m.Title.Equals(titleName.Trim())).FirstOrDefault();
                            if (colModel != null)
                                titleName = colModel.Field;
                            var val = sheet.Cells[i, col].Value == null
                                ? string.Empty
                                : sheet.Cells[i, col].Value.ToString();
                            tempsb.Append("\"" + titleName + "\":\"" +
                                          val.Replace("\"", "\\\"").Replace("	", "").Replace("\\", "/").Trim() + "\"");
                            tempsb.Append(",");
                            if (col == columnCount)
                                if (tempsb.Length > 0)
                                    tempsb.Remove(tempsb.Length - 1, 1);
                        }
                        tempsb.Append("},");
                        if (tempsb.ToString().IndexOf("{}") > -1)
                        {
                            tempsb.Clear();
                        }
                        else
                        {
                            sb.Append(tempsb);
                            tempsb.Clear();
                        }
                        if (i == rowCount)
                            if (sb.Length > 1)
                                sb.Remove(sb.Length - 1, 1);
                    }
                    sb.Append("]");
                }
            }
            return sb.ToString();
            ;
        }

        public static string DataTableToJson(DataTable dt)
        {
            if (dt == null)
                return "[]";
            var builder = new StringBuilder();
            builder.Append("[");
            if (dt.Rows.Count > 0)
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append("{");
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        builder.Append("\"" + dt.Columns[j].ColumnName + "\":\"" + dt.Rows[i][j].ToString()
                                           .Replace("\"", "\\\"").Replace("	", "").Replace("\\", "/") + "\"");
                        if (j < dt.Columns.Count - 1)
                            builder.Append(",");
                    }
                    builder.Append("}");
                    if (i < dt.Rows.Count - 1)
                        builder.Append(",");
                }
            builder.Append("]");
            return builder.ToString();
        }
    }


    public class DataGridColumns
    {
        public string Field { get; set; }

        public string Title { get; set; }
    }
}