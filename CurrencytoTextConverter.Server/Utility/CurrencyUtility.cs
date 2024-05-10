using System.Globalization;
using CurrencytoTextConverter.Server.Mapping;

namespace CurrencytoTextConverter.Server.Utility
{
    //This utility class contains the logic to convert a currency amount to text.
    //The MainConverter method is the main method that converts the currency amount to text.
    public class CurrencyUtility
    {
        private readonly CurrencyMapping _currencyMapping;

        public CurrencyUtility(CurrencyMapping currencyMapping)
        {
            _currencyMapping = currencyMapping;
        }

        public async Task<string> DecimalConverter(decimal amount)
        {
            var wholeNumber = amount.ToString(CultureInfo.InvariantCulture).Split('.')[0];
            var decimalNumber = amount.ToString(CultureInfo.InvariantCulture).Split('.')[1];
            
            if (Convert.ToDecimal(wholeNumber) == 0 && Convert.ToDecimal(decimalNumber) != 0)
            {
                return $"{await MainConverter(Convert.ToDecimal(decimalNumber))} cents";
            }
            var result = decimalNumber != "0" ? $"{await MainConverter(Convert.ToDecimal(wholeNumber))} and {await MainConverter(Convert.ToDecimal(decimalNumber))} cents" : $"{await MainConverter(Convert.ToDecimal(wholeNumber))} dollars";
            return result;
        }

        private Task<string> OnesConverter(decimal amount)
        {
            var value = Convert.ToInt32(amount);
            var result = "";

            if (_currencyMapping.OnesDictionary.TryGetValue(value, out var value1))
            {
                result = value1;
            }
            return Task.FromResult(result);
        }

        private async Task<string> TensConverter(decimal amount)
        {
            var value = Convert.ToInt32(amount);
            string result;

            if (_currencyMapping.TensDictionary.TryGetValue(value, out var value1))
            {
                result = value1;
            }
            else
            {
                result = await TensConverter(value - value % 10) + "-" + await OnesConverter(value % 10);
            }
            return result;
        }

        public async Task<string> MainConverter(decimal amount)
        {
            var value = Convert.ToDouble(amount);
            var result = "";
            var isFinished = false;
            if (!(value > 0)) return result;
            var amountLength = value.ToString(CultureInfo.InvariantCulture).Length;
            var placeValue = "";
            var position = 0;

            switch (amountLength)
            {
                case 1:
                    result = await OnesConverter((decimal)value);
                    isFinished = true;
                    break;
                case 2:
                    result = await TensConverter((decimal)value);
                    isFinished = true;
                    break;
                case 3:
                    position = 1;
                    placeValue = "Hundred";
                    break;
                case 4:
                case 5:
                case 6:
                    position = (amountLength % 4) + 1;
                    placeValue = "Thousand";
                    break;
                case 7:
                case 8:
                case 9:
                    position = (amountLength % 7) + 1;
                    placeValue = "Million";
                    break;
                case 10:
                case 11:
                case 12:
                    position = (amountLength % 10) + 1;
                    placeValue = "Billion";
                    break;
                case 13:
                case 14:
                case 15:
                    position = (amountLength % 13) + 1;
                    placeValue = "Trillion";
                    break;
                default:
                    return "";
            }

            if (isFinished) return result;
            if (value.ToString(CultureInfo.InvariantCulture)[..position] != "0" && value.ToString(CultureInfo.InvariantCulture)[position..] != "0")
            {
                result = await MainConverter(Convert.ToDecimal(value.ToString(CultureInfo.InvariantCulture)[..position])) 
                         + " " + placeValue + " " 
                         + await MainConverter(Convert.ToDecimal(value.ToString(CultureInfo.InvariantCulture)[position..]));
            }
            else
            {
                result = await MainConverter(Convert.ToDecimal(value.ToString(CultureInfo.InvariantCulture)[..position])) 
                         + " " + await MainConverter(Convert.ToDecimal(value.ToString(CultureInfo.InvariantCulture)[position..]));
            }
            return result;
        }
    }
}