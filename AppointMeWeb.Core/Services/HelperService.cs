﻿using AppointMeWeb.Core.Contracts;

namespace AppointMeWeb.Core.Services
{
    public class HelperService : IHelperService
    {
        /// <summary>
        /// Retrieves a list of days in the week, starting from Monday and ensuring Sunday is at the end of the list.
        /// This method skips the first day (Sunday) from the Enum and appends it to the end of the collection.
        /// </summary>
        /// <returns>A list of <see cref="DayOfWeek"/> starting from Monday and ending with Sunday.</returns>
        public List<DayOfWeek> GetDaysOfWeek()
        {
            return Enum.GetValues(typeof(DayOfWeek))
                   .Cast<DayOfWeek>()
                   .Skip(1)
                   .Concat(new[] { DayOfWeek.Sunday })
                   .ToList();
        }

        /// <summary>
        /// Retrieves all values of a specified enum type.
        /// </summary>
        public IEnumerable<T> GetEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public IEnumerable<string> GetEnumValuesAsString<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(e => e.ToString());
        }
        public List<DateOnly> GetNextCountOfDays(int countOfDays)
        {
            var dates = new List<DateOnly>();
            DateOnly startDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

            for (int i = 0; i < countOfDays; i++)
            {
                dates.Add(startDate.AddDays(i));
            }

            return dates;
        }
    }
}
