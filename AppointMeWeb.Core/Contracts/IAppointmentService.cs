using AppointMeWeb.Core.Enums;
using AppointMeWeb.Core.Models.AppointmeModels;
using AppointMeWeb.Core.Models.ClientRecordsModels;
using AppointMeWeb.Core.Models.HomeModels;
using AppointMeWeb.Core.Models.Schedule;

namespace AppointMeWeb.Core.Contracts
{
    public interface IAppointmentService
    {
        Dictionary<DateOnly, string> WorkingHours { get; }
        Dictionary<DateOnly, string> TooltipTexts { get; }

        /// <summary>
        /// Cancels an appointment by updating its status to "Canceled" based on the provided appointment ID
        /// and user or business identifier. 
        /// </summary>
        /// <typeparam name="T">The type of the identifier, which can be either a string for User ID 
        /// or an int for Business Service Provider ID.</typeparam>
        /// <param name="id">The user or business identifier (string for User ID, int for Business ID).</param>
        /// <param name="appointmentId">The ID of the appointment to cancel.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains 
        /// true if the appointment was successfully canceled, or false if no matching appointment was found.</returns>
        /// <exception cref="InvalidOperationException">Thrown if an error occurs while attempting to 
        /// cancel the appointment.</exception>
        Task<bool> CancelAppointmentAsync<T>(T id, int appointmentId);
        Task<IEnumerable<BusinessAppointmentViewModel>> GetAppointmentsAsync(string userId, 
                    AppointmentSearchCriteria criteria,
                    DateRange? range = null);
        Task<Dictionary<DateOnly, List<AppointmentSlotViewModel>>> GetAvaibleSlotsAsync(int businessId);
        Task<IEnumerable<ClientRecordViewModel>> GetClientAppointmentsByEmailAndTermAsync(string searchByEmail, string businessUserId);
        Task<UserHomeIndexView> GetUserAppointmentsAsync(string userId);
    }
}
