using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.BuilderProfile
{
	public class ProfileOfDealersViewModel
	{
		public int ContractsId { get; set; }
		public string TypeContact { get; set; }
		public int? ContractType { get; set; }
		public int? FilingUserDetailsId { get; set; }
		public int? ApplicantDetailsId { get; set; }
		public string Name { get; set; }
		public string Family { get; set; }
		public string FullName { get; set; }
		public string Mobile { get; set; }
		public string ContractNumber { get; set; }
		public string ContractManagerId { get; set; }
		public DateTime? ContractStartDate { get; set; }
		public DateTime? ContractEndDate { get; set; }
		public DateTime? BuildingDeliveryDate { get; set; }
		public DateTime? TransferDateOfTheDocument { get; set; }
		public string? ContractStartDateFarsi { get; set; }
		public string? ContractEndDateFarsi { get; set; }
		public string? BuildingDeliveryDateFarsi { get; set; }
		public string? TransferDateOfTheDocumentFarsi { get; set; }
	}
}
