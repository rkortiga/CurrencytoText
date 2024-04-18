using System;
using CurrencytoTextConverter.Server.Mapping;

namespace CurrencytoTextConverter.Server.Utility
{
    public class CurrencyUtility
    {
        private readonly CurrencyMapping _currencyMapping;

        public CurrencyUtility(CurrencyMapping currencyMapping)
        {
            _currencyMapping = currencyMapping;
        }

        public async Task<string> DecimalConverter(decimal amount)
        {
            var wholeNumber = amount.ToString().Split('.')[0];
            var decimalNumber = amount.ToString().Split('.')[1];
            string result;

            if (decimalNumber != "0")
            {
                result = $"{await MainConverter(Convert.ToDecimal(wholeNumber))} and {await MainConverter(Convert.ToDecimal(decimalNumber))} cents";
            }
            else
            {
                result = $"{await MainConverter(Convert.ToDecimal(wholeNumber))} dollars";
            }
            return result;
        }

        public async Task<string> OnesConverter(decimal amount)
        {
            var value = Convert.ToInt32(amount);
            string result = "";

            if (_currencyMapping.OnesDictionary.ContainsKey(value))
            {
                result = _currencyMapping.OnesDictionary[value];
            }
            return result;
        }

        public async Task<string> TensConverter(decimal amount)
        {
            var value = Convert.ToInt32(amount);
            string result;

            if (_currencyMapping.TensDictionary.ContainsKey(value))
            {
                result = _currencyMapping.TensDictionary[value];
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
            string result = "";
            bool isFinished = false;
            if (value > 0)
            {
                int numLength = value.ToString().Length;
                string placeValue = "";
                int position = 0;

                switch (numLength)
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
                        position = (numLength % 4) + 1;
                        placeValue = "Thousand";
                        break;
                    case 7:
                    case 8:
                    case 9:
                        position = (numLength % 7) + 1;
                        placeValue = "Million";
                        break;
                    case 10:
                    case 11:
                    case 12:
                        position = (numLength % 10) + 1;
                        placeValue = "Billion";
                        break;
                    case 13:
                    case 14:
                    case 15:
                        position = (numLength % 13) + 1;
                        placeValue = "Trillion";
                        break;
                    default:
                        return "";
                }

                if (!isFinished)
                {
                    if (value.ToString()[..position] != "0" && value.ToString()[position..] != "0")
                    {
                        result = await MainConverter(Convert.ToDecimal(value.ToString()[..position])) + " " + placeValue + " " + await MainConverter(Convert.ToDecimal(value.ToString()[position..]));
                    }
                    else
                    {
                        result = await MainConverter(Convert.ToDecimal(value.ToString()[..position])) + " " + await MainConverter(Convert.ToDecimal(value.ToString()[position..]));
                    }
                }

            }
            return result;
        }
    }
}
