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

        public string SliceAmount(Currency currency)
        {
            string result;

            var integerPart = (long)Math.Truncate(currency.Amount);
            var fractionalPart = currency.Amount - integerPart;

            var integerText = _utility.MainConverter(integerPart);

            var decimalText = string.Empty;
            if (fractionalPart > 0)
            {
                decimalText = _utility.DecimalConverter(fractionalPart);
            }

            if (integerPart == 1)
            {
                result = $"{integerText} dollar {decimalText}";
            }
            else if (!string.IsNullOrWhiteSpace(integerText) && !string.IsNullOrWhiteSpace(decimalText))
            {
                result = $"{integerText} dollars {decimalText}";
            }
            else if (!string.IsNullOrWhiteSpace(integerText))
            {
                result = $"{integerText} dollars";
            }
            else if (!string.IsNullOrWhiteSpace(decimalText))
            {
                result = decimalText;
            }
            else
            {
                result = "Zero";
            }
            return result;
        }
    }
}
