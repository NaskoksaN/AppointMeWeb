﻿
namespace AppointMeWeb.Core.Contracts
{
    public interface IHelperService
    {
        /// <summary>
        /// Retrieves a list of days in the week, starting from Monday and ensuring Sunday is at the end of the list.
        /// This method skips the first day (Sunday) from the Enum and appends it to the end of the collection.
        /// </summary>
        /// <returns>A list of <see cref="DayOfWeek"/> starting from Monday and ending with Sunday.</returns>
        List<DayOfWeek> GetDaysOfWeek();
    }
}
