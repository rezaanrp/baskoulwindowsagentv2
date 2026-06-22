using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IExcelExporter
    {
        byte[] Export<T>(
            List<T> data,
            string sheetName,
            List<string> headers,
            List<Func<T, object>> valueSelectors);
    }
}

