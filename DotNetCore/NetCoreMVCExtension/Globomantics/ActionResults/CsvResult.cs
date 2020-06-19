using Globomantics.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globomantics.ActionResults
{
    public class CsvResult : IActionResult
    {
        private IEnumerable _sourceData;
        private string _fileName;

        public CsvResult(IEnumerable data, string fileName)
        {
            this._sourceData = data;
            this._fileName = fileName;

        }
        public Task ExecuteResultAsync(ActionContext context)
        {
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);
            foreach(var rate in _sourceData)
            {
                var properties = rate.GetType().GetProperties();
                foreach(var prop in properties)
                {
                    writer.Write(FinderPropertyValue(rate, prop.Name));
                    writer.Write(",");
                }
                writer.WriteLine();
            }

            var csvBytes = Encoding.ASCII.GetBytes(writer.ToString());
            context.HttpContext.Response.Headers["content-disposition"] = "attachment;filename=" + _fileName + ".csv";
            return context.HttpContext.Response.Body.WriteAsync(csvBytes, 0, csvBytes.Length);
        }

        private string FinderPropertyValue(object item, string prop)
        {
            return item.GetType().GetProperty(prop).GetValue(item, null).ToString() ?? "";
        }
    }
}
