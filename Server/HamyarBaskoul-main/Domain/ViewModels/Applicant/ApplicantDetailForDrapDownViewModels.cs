using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Applicant
{
	public class ApplicantDetailForDrapDownViewModels
	{
		public int Id { get; set; }
		[MaxLength(100)]
		public string? Name { get; set; }
		[MaxLength(100)]
		public string? Family { get; set; }
		[MaxLength(11)]
		public string? Mobile { get; set; }
		[MaxLength(11)]
		public string? Tel { get; set; }
	}
}
