using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string userName { get; set; }
        public string Family { get; set; }
        public string Mobile { get; set; }
        public bool IsDelete { get; set; }
        public string CodMarkaz { get; set; }
        public int? SelectedSiteId { get; set; }
    }

}
