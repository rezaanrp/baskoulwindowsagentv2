using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tools
{
	public class csShamciToMiladi
	{
		public DateTime ShamciToMiladi(string shamci)
		{
			if (string.IsNullOrWhiteSpace(shamci))
			{
				DateTime today = DateTime.Today;
				return today;
			}
			string[] da = shamci.Split("/");
			PersianCalendar pc = new PersianCalendar();
			DateTime dt = new DateTime(int.Parse(da[0]), int.Parse(da[1]), int.Parse(da[2]), pc);
			return dt;
		}
		public string MiladiToShamci(DateTime? dt)
		{
			DateTime dtnu ;
			if (dt == null)
			{
				dt = DateTime.Now;
			}
			dtnu = (DateTime)dt;

            if (dtnu.Year > 1000)
			{
				PersianCalendar pc = new PersianCalendar();
				string PersianDate = string.Format("{0}/{1}/{2}", pc.GetYear(dtnu), pc.GetMonth(dtnu), pc.GetDayOfMonth(dtnu));

				return PersianDate;
			}
			else
			{
				DateTime today = DateTime.Today;
				PersianCalendar pc = new PersianCalendar();
				string PersianDate = string.Format("{0}/{1}/{2}", pc.GetYear(today), pc.GetMonth(today), pc.GetDayOfMonth(today));

				return PersianDate;
			}

		}
	}
}
