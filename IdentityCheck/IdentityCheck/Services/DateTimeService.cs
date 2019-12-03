using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityCheck.Models;

namespace IdentityCheck.Services
{
    public class DateTimeService : IDateTimeService
    {

        private readonly IUserService userService;

        public DateTimeService(IUserService userService)
        {
            this.userService = userService;
        }

        public IEnumerable<TimeZoneInfo> GetAvailableTimeZones()
        {
            return TimeZoneInfo.GetSystemTimeZones();
        }

        public DateTime GetLocalDateTime(ApplicationUser user, DateTime utcDateTime)
        {
            if (user?.TimeZoneId == null)
            {
                return utcDateTime;
            }

            var timezoneinfo = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timezoneinfo);
        }

        public async Task SetUserTimeZoneAsync(ApplicationUser user, string timeZoneId)
        {
            if (!IsValidTimeZoneId(timeZoneId))
            {
                return;
            }

            user.TimeZoneId = timeZoneId;
            await userService.SaveUserAsync(user);
        }

        private bool IsValidTimeZoneId(string timeZoneId)
        {

            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return true;
            }
            catch (TimeZoneNotFoundException)
            {
                return false;
            }
        }

        public string TimeAgo(ApplicationUser user, DateTime postDateTime)
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZoneId);
            var postDate = TimeZoneInfo.ConvertTimeFromUtc(postDateTime, userTimeZone);
            var localDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, userTimeZone);
            TimeSpan span = localDate - postDate;

            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("about {0} {1} ago", years, years == 1 ? "year" : "years");
            }

            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("about {0} {1} ago", months, months == 1 ? "month" : "months");
            }

            if (span.Days > 0)
                return String.Format("about {0} {1} ago", span.Days, span.Days == 1 ? "day" : "days");

            if (span.Hours > 0)
                return String.Format("about {0} {1} ago", span.Hours, span.Hours == 1 ? "hour" : "hours");

            if (span.Minutes > 0)
                return String.Format("about {0} {1} ago", span.Minutes, span.Minutes == 1 ? "minute" : "minutes");

            if (span.Seconds >= 5)
                return String.Format("about {0} seconds ago", span.Seconds);

            if (span.Seconds < 5)
                return "just now";

            return string.Empty;
        }
    }
}
