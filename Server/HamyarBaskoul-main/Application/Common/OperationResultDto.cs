using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
	public class OperationResultDto<T>
	{
		public bool Success { get; set; } = true;
		public string Message { get; set; } = string.Empty;
		public T? Data { get; set; }

		// Convenience methods
		public static OperationResultDto<T> Ok(T data, string message = "") =>
			new OperationResultDto<T> { Success = true, Data = data, Message = message };

		public static OperationResultDto<T> Fail(string message) =>
			new OperationResultDto<T> { Success = false, Message = message };
	}
}


