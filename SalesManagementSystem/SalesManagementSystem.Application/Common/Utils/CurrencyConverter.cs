using System.Globalization;

namespace SalesManagementSystem.Application.Common.Utils;

public class CurrencyConverter
{
    protected CurrencyConverter()
    {
        
    }
    /// <summary>
    /// Converts the specified integer value to a formatted currency string based on the current culture.
    /// </summary>
    /// <param name="value">The integer value to be converted.</param>
    /// <returns>A formatted currency string without the currency symbol.</returns>
    public static string ConvertToCurrencyString(int value)
    {
        CultureInfo currentCulture = CultureInfo.CurrentCulture;
        NumberFormatInfo formatInfo = (NumberFormatInfo)currentCulture.NumberFormat.Clone();
        // Set an empty string to remove the currency symbol
        formatInfo.CurrencySymbol = "";  
        
        // Using "C0" to format without decimals
        return value.ToString("C0", formatInfo);

    }
}