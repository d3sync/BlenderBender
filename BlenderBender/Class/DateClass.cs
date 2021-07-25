using System;

namespace BlenderBender
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
                    meh = meh.AddDays(nn);
                    break;
                case "bookExcludeSunday":
                    for (int i=1; i < 7; i++)
                    {
                        meh = meh.AddDays(1);
                        if (meh.DayOfWeek == DayOfWeek.Sunday) { meh = meh .AddDays(1); }
                    }
                    break;
                default:
                    meh = meh.AddDays(nn);
                    break;
            }
            if (extraDays != 0) { 
                meh = meh.AddDays(extraDays);  
                if (meh.DayOfWeek == DayOfWeek.Sunday) { meh = meh.AddDays(1); }
            }
            var dtp = meh.ToString("dddd dd/MM");
            var ntay = meh.DayOfWeek.ToString();
            switch (ntay)
            {
                case "Monday":
                    return dtp.Replace(ntay, "ΤΗΝ ΔΕΥΤΕΡΑ");
                    
                case "Tuesday":
                    return dtp.Replace(ntay, "ΤΗΝ ΤΡΙΤΗ");
                    
                case "Wednesday": 
                    return dtp.Replace(ntay, "ΤΗΝ ΤΕΤΑΡΤΗ");
                    
                case "Thursday":
                    return dtp.Replace(ntay, "ΤΗΝ ΠΕΜΠΤΗ");
                    
                case "Friday":
                    return dtp.Replace(ntay, "ΤΗΝ ΠΑΡΑΣΚΕΥΗ");
                    
                case "Saturday":
                    return dtp.Replace(ntay, "ΤΟ ΣΑΒΒΑΤΟ");
                    
                default:
                    return dtp;
                    
            }
        }
    }
}
