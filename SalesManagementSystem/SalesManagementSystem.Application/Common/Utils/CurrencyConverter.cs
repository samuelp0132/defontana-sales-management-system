using System.Globalization;

namespace SalesManagementSystem.Application.Common.Utils;

public static class CurrencyConverter
{
        public static string ConvertToCurrencyString(int value)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            NumberFormatInfo formatInfo = (NumberFormatInfo)currentCulture.NumberFormat.Clone();
            formatInfo.CurrencySymbol = "";  // Set an empty string to remove the currency symbol            return value.ToString("C", currentCulture);
            
            return value.ToString("C0", formatInfo);  // Using "C0" to format without decimals

        }
}