using System.Runtime.CompilerServices;

namespace SmartSchool.API.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurranceAge(this DateTime dateTime)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTime.Year;

            if (currentDate < dateTime.AddYears(age))
                age--;

            return age;
        }
    }
}
