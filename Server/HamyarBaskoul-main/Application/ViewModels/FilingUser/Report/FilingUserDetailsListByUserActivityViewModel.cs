using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.ViewModels.FilingUser.Report
{
	public class FilingUserDetailsListByUserActivityViewModel
	{
		public string Id { get; set; }
		public string? Name { get; set; }
		public string? Family { get; set; }
		public int? FileCount { get; set; }
		public int? FileType15 { get; set; }
		public int? FileType16 { get; set; }
		public int? FileType17 { get; set; }
        public int? FileType18 { get; set; }
        public int? FileType19 { get; set; }
        public string? TransactionType { get; set; }
		public int? totalnumberofads { get; set; }
		public int? Totalnumberofundergraduates { get; set; }
		public int? apartment { get; set; }
		public int? villa { get; set; }
		public int? Kalangi { get; set; }
		public int? Earth { get; set; }
		public int? Participation { get; set; }
		public int? Commercial { get; set; }
		public int? Administrative { get; set; }
		public int? Industrialandrealestate { get; set; }
		public int? Numberofimagesubmissions { get; set; }

	}
}
