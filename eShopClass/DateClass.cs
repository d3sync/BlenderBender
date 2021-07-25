using System;

namespace eShopClass
{
    public class DateClass
    {
        public string DateTo(string option,int extraDays)
        {
            var nn = 2;
            var meh = DateTime.Now;
            switch (option)
            {
                case "excludeSunday":
                    if ((meh.DayOfWeek == DayOfWeek.Saturday) || (meh.DayOfWeek == DayOfWeek.Friday)) nn += 1;
                    meh.AddDays(nn);
                    break;
                case "bookExcludeSunday":
                    for (int i=1; i < 7; i++)
                    {
                        meh.AddDays(1);
                        if (meh.DayOfWeek == DayOfWeek.Sunday) { meh.AddDays(1); }
                    }
                    break;
                default:
                    meh.AddDays(nn);
                    break;
            }
            if (extraDays != 0) { 
                meh.AddDays(extraDays);  
                if (meh.DayOfWeek == DayOfWeek.Sunday) { meh.AddDays(1); }
            }
            var dtp = meh.ToString("dddd dd/MM");
            var ntay = meh.DayOfWeek.ToString();
            switch (ntay)
            {
                case "Monday":
                    return dtp.Replace(ntay, "ΤΗΝ ΔΕΥΤΕΡΑ");
                    break;
                case "Tuesday":
                    return dtp.Replace(ntay, "ΤΗΝ ΤΡΙΤΗ");
                    break;
                case "Wednesday": 
                    return dtp.Replace(ntay, "ΤΗΝ ΤΕΤΑΡΤΗ");
                    break;
                case "Thursday":
                    return dtp.Replace(ntay, "ΤΗΝ ΠΕΜΠΤΗ");
                    break;
                case "Friday":
                    return dtp.Replace(ntay, "ΤΗΝ ΠΑΡΑΣΚΕΥΗ");
                    break;
                case "Saturday":
                    return dtp.Replace(ntay, "ΤΟ ΣΑΒΒΑΤΟ");
                    break;
                default:
                    return dtp;
                    break;
            }
        }
    }
}
