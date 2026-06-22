using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Application.Common.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class RequiredIfSessionTypeAttribute : ValidationAttribute
	{
		private readonly int _targetSessionType;
		private readonly string _sessionTypePropertyName;

		public RequiredIfSessionTypeAttribute(string sessionTypePropertyName, int targetSessionType)
		{
			_sessionTypePropertyName = sessionTypePropertyName;
			_targetSessionType = targetSessionType;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			PropertyInfo sessionTypeProperty = validationContext.ObjectType.GetProperty(_sessionTypePropertyName);
			if (sessionTypeProperty == null)
				return new ValidationResult($"Property '{_sessionTypePropertyName}' not found.");

			var sessionTypeValue = sessionTypeProperty.GetValue(validationContext.ObjectInstance, null);

			if (sessionTypeValue is int type && type == _targetSessionType)
			{
				if (value == null)
					return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} الزامی است.");
			}

			return ValidationResult.Success;
		}
	}
}

