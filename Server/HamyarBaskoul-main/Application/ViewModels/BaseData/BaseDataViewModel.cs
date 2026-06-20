using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.BaseData
{
    public class BaseDataViewModel
    {
        public int ID { get; set; }
        public string Onvan { get; set; }
        public string? Tozihat { get; set; }
        public string? TableName { get; set; }
    }
}
