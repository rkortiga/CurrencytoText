using CurrencytoTextConverter.Server.Model;
using CurrencytoTextConverter.Server.Utility;

namespace CurrencytoTextConverter.Server.Helper
{
	//This helper class contains the SliceAmount method that is used to slice the currency amount into its integer and fractional parts.
	//It then converts the integer and fractional parts to text using methods from the Utility class.
	public class CurrencyHelper
	{
		private readonly CurrencyUtility _utility;

		public CurrencyHelper(CurrencyUtility utility)
		{
			_utility = utility;
		}

		public async Task<string> SliceAmount(Currency currency)
		{
			var integerPart = (long)Math.Truncate(currency.Amount);
			var fractionalPart = currency.Amount - integerPart;

			if (integerPart.ToString().Length > 15)
			{
				return "Amount is too large.";
			}

			var integerText = await _utility.MainConverter(integerPart);
			var decimalText = fractionalPart > 0 ? await _utility.DecimalConverter(fractionalPart) : string.Empty;

			if (string.IsNullOrWhiteSpace(integerText) && string.IsNullOrWhiteSpace(decimalText))
			{
				return "Zero";
			}

			var result = new System.Text.StringBuilder();
			if (!string.IsNullOrWhiteSpace(integerText))
			{
				result.Append($"{integerText} {(integerPart == 1 ? "dollar" : "dollars")}");
			}

			if (!string.IsNullOrWhiteSpace(decimalText))
			{
				if (result.Length > 0)
				{
					result.Append(" and ");
				}
				result.Append(decimalText);
			}

			return result.ToString();
		}
	}
}