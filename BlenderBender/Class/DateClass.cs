using System;
using BlenderBender.Services;

namespace BlenderBender
{
    /// <summary>
    /// Handles date calculations with Greek language formatting and business day logic.
    /// </summary>
    public class DateClass : IDateService
    {
        /// <summary>
        /// Calculates a future date with Greek formatting based on specified options.
        /// </summary>
        /// <param name="option">Date calculation option (excludeSunday, bookExcludeSunday, includeSunday)</param>
        /// <param name="extraDays">Additional days to add</param>
        /// <returns>Formatted Greek date string</returns>
        public string DateTo(string option, int extraDays)
        {
            var businessDaysToAdd = 2;
            var targetDate = DateTime.Now;
            
            switch (option?.ToLowerInvariant())
            {
                case "excludesunday":
                    // If it's Friday or Saturday, add extra day to skip Sunday
                    if (targetDate.DayOfWeek == DayOfWeek.Saturday || targetDate.DayOfWeek == DayOfWeek.Friday) 
                        businessDaysToAdd += 1;
                    targetDate = targetDate.AddDays(businessDaysToAdd);
                    break;
                    
                case "bookexcludesunday":
                    // Add 6 days, skipping any Sundays encountered
                    for (var i = 1; i < 7; i++)
                    {
                        targetDate = targetDate.AddDays(1);
                        if (targetDate.DayOfWeek == DayOfWeek.Sunday) 
                            targetDate = targetDate.AddDays(1);
                    }
                    break;
                    
                case "includesunday":
                default:
                    targetDate = targetDate.AddDays(businessDaysToAdd);
                    break;
            }

            // Add extra days if specified
            targetDate = targetDate.AddDays(extraDays);
            
            // Skip Sunday if final date lands on it
            if (targetDate.DayOfWeek == DayOfWeek.Sunday) 
                targetDate = targetDate.AddDays(1);

            return FormatDateInGreek(targetDate);
        }

        /// <summary>
        /// Formats a DateTime to Greek language day and date format.
        /// </summary>
        /// <param name="date">Date to format</param>
        /// <returns>Greek formatted date string</returns>
        private string FormatDateInGreek(DateTime date)
        {
            var dateFormatted = date.ToString("dddd dd/MM");
            var dayOfWeek = date.DayOfWeek.ToString();
            
            return dayOfWeek switch
            {
                "Monday" => dateFormatted.Replace(dayOfWeek, "ΤΗΝ ΔΕΥΤΕΡΑ"),
                "Tuesday" => dateFormatted.Replace(dayOfWeek, "ΤΗΝ ΤΡΙΤΗ"),
                "Wednesday" => dateFormatted.Replace(dayOfWeek, "ΤΗΝ ΤΕΤΑΡΤΗ"),
                "Thursday" => dateFormatted.Replace(dayOfWeek, "ΤΗΝ ΠΕΜΠΤΗ"),
                "Friday" => dateFormatted.Replace(dayOfWeek, "ΤΗΝ ΠΑΡΑΣΚΕΥΗ"),
                "Saturday" => dateFormatted.Replace(dayOfWeek, "ΤΟ ΣΑΒΒΑΤΟ"),
                _ => dateFormatted
            };
        }
    }
}