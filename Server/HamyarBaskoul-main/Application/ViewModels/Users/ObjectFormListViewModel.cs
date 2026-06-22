using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Users
{
	public class ObjectFormListViewModel
	{
        public string  UserId { get; set; }
        public List<ObjectFormViewModel>  objectFormViews { get; set; }
	}
}

