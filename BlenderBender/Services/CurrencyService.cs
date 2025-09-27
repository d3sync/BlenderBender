using System;
using System.Collections.Generic;
using System.Globalization;

namespace BlenderBender.Services
{
    /// <summary>
    /// Service responsible for currency calculations and denominations.
    /// Extracted from Form1.cs button8_Click method to improve maintainability.
    /// </summary>
    public class CurrencyService
    {
        private readonly CultureInfo _culture;
        private readonly NumberStyles _numberStyles;

        // Currency denominations configuration
        private readonly Dictionary<int, int> _integerDenominations = new Dictionary<int, int>
        {
            { 500, 500 },
            { 200, 200 },
            { 100, 100 },
            { 50, 50 },
            { 20, 20 },
            { 10, 10 },
            { 5, 5 },
            { 2, 2 },
            { 1, 1 }
        };

        private readonly Dictionary<int, double> _decimalDenominations = new Dictionary<int, double>
        {
            { 50, 0.50 },
            { 20, 0.20 },
            { 10, 0.10 },
            { 5, 0.05 },
            { 2, 0.02 },
            { 1, 0.01 }
        };

        public CurrencyService(CultureInfo culture = null, NumberStyles numberStyles = NumberStyles.AllowDecimalPoint)
        {
            _culture = culture ?? CultureInfo.CurrentCulture;
            _numberStyles = numberStyles;
        }

        /// <summary>
        /// Calculates the total value for integer denominations.
        /// </summary>
        /// <param name="counts">Dictionary mapping denomination to count</param>
        /// <returns>Dictionary mapping denomination to total value</returns>
        public Dictionary<int, string> CalculateIntegerDenominations(Dictionary<int, string> counts)
        {
            var results = new Dictionary<int, string>();

            foreach (var denomination in _integerDenominations)
            {
                if (counts.ContainsKey(denomination.Key) && !string.IsNullOrEmpty(counts[denomination.Key]))
                {
                    if (int.TryParse(counts[denomination.Key], out int count))
                    {
                        var total = denomination.Value * count;
                        results[denomination.Key] = total.ToString();
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Calculates the total value for decimal denominations.
        /// </summary>
        /// <param name="counts">Dictionary mapping denomination to count</param>
        /// <returns>Dictionary mapping denomination to total value</returns>
        public Dictionary<int, string> CalculateDecimalDenominations(Dictionary<int, string> counts)
        {
            var results = new Dictionary<int, string>();

            foreach (var denomination in _decimalDenominations)
            {
                if (counts.ContainsKey(denomination.Key) && !string.IsNullOrEmpty(counts[denomination.Key]))
                {
                    if (double.TryParse(counts[denomination.Key], _numberStyles, _culture, out double count))
                    {
                        var total = denomination.Value * count;
                        results[denomination.Key] = total.ToString();
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Calculates the grand total from all denominations.
        /// </summary>
        /// <param name="integerResults">Results from integer denominations</param>
        /// <param name="decimalResults">Results from decimal denominations</param>
        /// <returns>The total sum as a double</returns>
        public double CalculateGrandTotal(Dictionary<int, string> integerResults, Dictionary<int, string> decimalResults)
        {
            double total = 0.0;

            // Sum integer denomination results
            foreach (var result in integerResults.Values)
            {
                if (double.TryParse(result, out double value))
                {
                    total += value;
                }
            }

            // Sum decimal denomination results
            foreach (var result in decimalResults.Values)
            {
                if (double.TryParse(result, out double value))
                {
                    total += value;
                }
            }

            return total;
        }

        /// <summary>
        /// Gets all available integer denominations.
        /// </summary>
        public IReadOnlyDictionary<int, int> IntegerDenominations => _integerDenominations;

        /// <summary>
        /// Gets all available decimal denominations.
        /// </summary>
        public IReadOnlyDictionary<int, double> DecimalDenominations => _decimalDenominations;
    }
}