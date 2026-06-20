using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.BaseData
{
    public class BaseDataDmainViewModel
    {
        public int ID { get; set; }
        public string Onvan { get; set; }
        public string? Tozihat { get; set; }
        public string CodMarkaz { get; set; }
        public string TableName { get; set; }
    }
}
