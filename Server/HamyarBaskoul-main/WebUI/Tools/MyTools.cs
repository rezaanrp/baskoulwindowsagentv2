using System.Text.RegularExpressions;

namespace WebUI.Tools
{
	public class MyTools
	{
		public string string_to_number(string? st)
		{
			if (st == null || st == string.Empty)
				return "0";
			st = st.Replace(",", "");

			if (Regex.IsMatch(st, @"\d"))
				return st;
			else
				return "0";
		}
	}
}
