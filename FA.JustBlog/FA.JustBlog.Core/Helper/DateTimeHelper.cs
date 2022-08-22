namespace FA.JustBlog.Core.Helper
{
    public static class DateTimeHelper
    {
        public static string TimeCompareToNow(this DateTime time)
        {
            var now = DateTime.Now;
            var timeDiff = now - time;
            if (now.Day - time.Day == 1 && now.Month == time.Month && time.Year == now.Year)
            {
                return $"yesterday at {time.Hour}:{time.Minute}";
            }
            else if (now.Day - time.Day == 0 && now.Month == time.Month && time.Year == now.Year)
            {
                return $"{now.Subtract(time).ToString("hh-mm")} ago";
            }
            return $"{((int)timeDiff.TotalDays)} days ago";
        }
    }
}
