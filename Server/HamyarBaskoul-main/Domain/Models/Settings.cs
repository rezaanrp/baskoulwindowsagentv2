using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Settings : AuditableEntity
    {
        [Key]
        public int ID { get; set; }
        public string? SettingName { get; set; }
        public double? SettingValue { get; set; }
        public int? SettingParameter { get; set; }
        public string? Sal { get; set; }
        public bool? Delet { get; set; }
        public string? Comp { get; set; }
        public bool? Defaults { get; set; }
        public string? Karbar_Ins { get; set; }
        public string? Karbar_Up { get; set; }
        public DateTime? Date_Ins { get; set; }
        public DateTime? Date_Up { get; set; }
        public string? Tozihat { get; set; }
        public string? CIP { get; set; }
        public long? dore_id { get; set; }
        public string? api_hash { get; set; }
        public string? CodMarkaz { get; set; }
    }
}
