using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geckonet.Core.Models;

namespace Geckonet.Core
{
    public static class UsefulExtensions
    {
        public static DateTime GetQuarterDateTime(this string quarter)
        {
            var quarterParts = quarter.Split(' ');
            int year = int.Parse(quarterParts[0]);
            int quarterNum = int.Parse(quarterParts[1].TrimStart('Q'));
            int month = 1;
            switch (quarterNum)
            {
                case 1:
                    month = 1;
                    break;
                case 2:
                    month = 4;
                    break;
                case 3:
                    month = 7;
                    break;
                case 4:
                    month = 10;
                    break;
            }
            return new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
        }
        /// <summary>
        /// Method of generically converting datatable to 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static GeckoDataset ConvertToDataset(this DataTable dataTable)
        {
            var retVal = new GeckoDataset();

            var fields = new Dictionary<string, IDatasetField>();
            foreach (DataColumn dataTableColumn in dataTable.Columns)
            {
                fields.Add(dataTableColumn.ColumnName.ToLowerInvariant(), new DatasetField(GetDatasetFieldType(dataTableColumn.DataType), dataTableColumn.ColumnName));
            }
            retVal.Fields = fields;

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (DataRow dataTableRow in dataTable.Rows)
            {
                Dictionary<string, object> rowData = new Dictionary<string, object>();
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var dataToInsert = dataTableRow[dataTable.Columns[i]];
                    rowData.Add(dataTable.Columns[i].ColumnName.ToLowerInvariant(), dataToInsert);
                }
                data.Add(rowData);
            }
            retVal.Data = data;

            return retVal;
        }

        public static DatasetFieldType GetDatasetFieldType(Type columnType)
        {
            if (columnType == typeof(short) || columnType == typeof(int) || columnType == typeof(long) || columnType == typeof(double) || columnType == typeof(decimal) || columnType == typeof(float))
            {
                return DatasetFieldType.number;
            }
            else if (columnType == typeof(DateTime))
            {
                return DatasetFieldType.datetime;
            }
            else
            {
                return DatasetFieldType.@string;
            }
        }
    }
}
