using Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Parameter
{
	public class ParameterViewModel
	{
		public int Id { get; set; }
		public string? UserId { get; set; }
		public int ParentId { get; set; }

		public string? sortColumn { get; set; }
		public string? sortColumnDirection { get; set; }
		public int? skip { get; set; }
		public int? pageSize { get; set; }
		public string? searchValue { get; set; }
		public bool? isSale { get; set; }
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
		public string? DateFromFarsi { get; set; }
		public string? DateToFarsi { get; set; }
		public Hashtable? hashtable { get;  set; }

	}
}

