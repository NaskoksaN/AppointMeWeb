﻿using Microsoft.EntityFrameworkCore;

using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Models.AppointmeModels;
using AppointMeWeb.Core.Models.BusinessProvider;
using AppointMeWeb.Core.Models.HomeModels;
using AppointMeWeb.Infrastrucure.Data.Common;
using AppointMeWeb.Infrastrucure.Data.Enum;
using AppointMeWeb.Infrastrucure.Data.Models;
using AppointMeWeb.Core.Models.FindService;
using AppointMeWeb.Core.Enums;
using Microsoft.IdentityModel.Tokens;


namespace AppointMeWeb.Core.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IRepository sqlService;
        private readonly List<ServiceInfoModels> businessInfo = new List<ServiceInfoModels>
            {
                new ServiceInfoModels { BusinessType = BusinessType.Doctor, ImageUrl = "https://plus.unsplash.com/premium_photo-1673953510197-0950d951c6d9?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Dentist, ImageUrl = "https://plus.unsplash.com/premium_photo-1675686363507-22a8d0e11b4c?q=80&w=1806&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Barber, ImageUrl = "https://images.unsplash.com/photo-1599351431613-18ef1fdd27e1?q=80&w=1888&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Hairdresser, ImageUrl = "https://plus.unsplash.com/premium_photo-1669675935483-01a22e5c88bf?q=80&w=1888&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Manicurist, ImageUrl = "https://images.unsplash.com/photo-1602585578130-c9076e09330d?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Lawyer, ImageUrl = "https://plus.unsplash.com/premium_photo-1661329930662-19a43503782f?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.CarService, ImageUrl = "https://images.unsplash.com/photo-1625047509248-ec889cbff17f?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Plumber, ImageUrl = "https://plus.unsplash.com/premium_photo-1663045495725-89f23b57cfc5?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Electrician, ImageUrl = "https://plus.unsplash.com/premium_photo-1661908782924-de673a5c6988?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Mechanic, ImageUrl = "https://plus.unsplash.com/premium_photo-1677009541474-1fc2642943c1?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Accountant, ImageUrl = "https://plus.unsplash.com/premium_photo-1678567671940-64eeefe22e5b?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Therapist, ImageUrl = "https://plus.unsplash.com/premium_photo-1663050739359-a4261779f6ba?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.PersonalTrainer, ImageUrl = "https://plus.unsplash.com/premium_photo-1672862927344-05ba0faa549b?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Teacher, ImageUrl = "https://plus.unsplash.com/premium_photo-1682888442432-a1bc427c0d91?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Photographer, ImageUrl = "https://plus.unsplash.com/premium_photo-1682098354910-6823d8fb2ac2?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Cleaner, ImageUrl = "https://images.unsplash.com/photo-1527515637462-cff94eecc1ac?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.Chef, ImageUrl = "https://images.unsplash.com/photo-1488992783499-418eb1f62d08?q=80&w=1889&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new ServiceInfoModels { BusinessType = BusinessType.MassageTherapist, ImageUrl = "https://plus.unsplash.com/premium_photo-1661266905734-22fb75b75ada?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"},
              };

        public BusinessService(IRepository _sqlService)
        {
            this.sqlService = _sqlService;
        }

        public async Task<bool> BookSlotAsync(BookSlotFormModel model, string currentUserId)
        {
            var business = await sqlService.All<BusinessServiceProvider>()
                        .Include(b => b.Appointments)
                        .Where(b => b.Id == model.BusinessId)
                        .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(BusinessServiceProvider));
            var appointmentCollection = business.Appointments;
            Appointment? slot = business.Appointments
                        .Where(a => a.StartTime == model.StartTime && a.EndTime == model.EndTime 
                        & a.Day==model.Date)
                        .FirstOrDefault();
            try
            {
                bool result = false;
                if (slot !=null && slot.IsBooked)
                {
                    return result;
                }
                Appointment newSlot = new()
                {
                    BusinessServiceProviderId = model.BusinessId,
                    UserId = currentUserId,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    IsBooked = true,
                    Day=model.Date,
                    Status=AppointmentStatus.Confirmed,
                };
                await sqlService.AddAsync<Appointment>(newSlot);
                await sqlService.SaveChangesAsync();
                result = true;

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while booking the slot.", ex);
            }
        }

        public List<ServiceInfoModels> BusinessInfo() => businessInfo;

       
        public async Task<BusinessQueryServiceModel> GetAllBusinessAsQueryAsync(
                        string? category = null, 
                        string? searchingTown = null, 
                        string? businessName = null, 
                        string? searchInDescription = null, 
                        int currentPage = 1, 
                        int businessPerPage = 1,
                        AlphabeticSort sorting = AlphabeticSort.Ascending)
        {
            IQueryable<BusinessServiceProvider> businessQuerry = sqlService
                        .AllReadOnly<BusinessServiceProvider>();
           
            if (!string.IsNullOrEmpty(category) && Enum.TryParse<BusinessType>(category, true, out var businessType))
            {
                businessQuerry = businessQuerry
                    .Where(b => b.BusinessType == businessType);
            }
            if (!string.IsNullOrEmpty(searchingTown))
            {
                businessQuerry = businessQuerry
                    .Where(b=>b.Town.ToLower().Contains(searchingTown.ToLower()));
            }
            if (!string.IsNullOrEmpty(businessName))
            {
                businessQuerry = businessQuerry
                    .Where(b => b.Name.ToLower().Contains(businessName.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchInDescription))
            {
                businessQuerry = businessQuerry
                    .Where(b => b.BusinessDescription.ToLower().Contains(searchInDescription.ToLower()));
            }

            businessQuerry = sorting switch
            {
                AlphabeticSort.Ascending => businessQuerry.OrderBy(b => b.Name),
                AlphabeticSort.Descending => businessQuerry.OrderByDescending(b => b.Name),
                _ => businessQuerry.OrderBy(b => b.Id)
            };

            var business = businessQuerry
                .Skip((currentPage-1) * businessPerPage)
                .Take(businessPerPage)
                .Select(b=> new BusinessViewModel()
                {

                    Id = b.Id,
                    Name = b.Name,
                    BusinessType = b.BusinessType.ToString(),
                    Description = b.BusinessDescription,
                    Phone = b.ApplicationUser.Phone,
                    Email = b.ApplicationUser.Email,
                    Town = b.Town,
                    Address = b.Address,
                    WebsiteUrl = b.Url,
                })
                .ToList();
            int totalBusinessCount = businessQuerry != null ? businessQuerry.Count() : 0;
            BusinessQueryServiceModel result =  new ()
            {
                CountOfBusiness = totalBusinessCount,
                Businesses = business
            };
            return result;
        }

        /// <summary>
        /// Asynchronously retrieves all business service provider records from the database,
        /// mapping them to a collection of <see cref="BusinessViewModel"/> objects.
        /// If no records are found or an error occurs during retrieval, a <see cref="DataAccessException"/> is thrown.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing an enumerable of <see cref="BusinessViewModel"/>.</returns>
        /// <exception cref="Exception">Thrown when no businesses are found or when a database access error occurs.</exception>
        public async Task<IEnumerable<BusinessViewModel>?> GetAllBusinessAsync()
        {
            try
            {
                IEnumerable<BusinessViewModel> result = await sqlService
                    .AllReadOnly<BusinessServiceProvider>()
                    .Select(b => new BusinessViewModel()
                    {
                        Id = b.Id,
                        Name = b.Name,
                        BusinessType = b.BusinessType.ToString(),
                        Description = b.BusinessDescription,
                        Phone = b.ApplicationUser.Phone,
                        Email = b.ApplicationUser.Email,
                        Town = b.Town,
                        Address = b.Address,
                        WebsiteUrl = b.Url,
                    })
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while accessing the database.", ex);
            }
        }

              
        public async Task<BusinessServiceProvider>? GetBusinessByIdAsync(int businessId)
        {
            try
            {
                BusinessServiceProvider? result = await sqlService
                            .AllReadOnly<BusinessServiceProvider>()
                            .FirstOrDefaultAsync(b => b.Id == businessId);
                return result;
            }
            catch (Exception)
            {
                throw new NullReferenceException("Business not found");
            }


        }

        public async Task<int> GetBusinessIdFromUserIdAsync(string userId)
        {
            BusinessServiceProvider? business = await sqlService
                    .All<BusinessServiceProvider>()
                    .Where(b=> b.ApplicationUserId==userId)
                    .FirstOrDefaultAsync();
            if (business == null)
            {
                throw new InvalidOperationException($"No business found for user with ID {userId}");
            }

            return business.Id;
        }

        /// <summary>
        /// Retrieves business statistics for a specific user, including average rating, 
        /// total monthly appointments, and new clients for the current month.
        /// </summary>
        /// <param name="userId">The ID of the user associated with the business.</param>
        /// <returns>
        /// A <see cref="BusinessStatisticsViewModel"/> containing the following data:
        /// - AverageRating: The average rating of all confirmed appointments up to yesterday.
        /// - TotalMonthlyAppointments: The count of confirmed appointments from the beginning of the month to yesterday.
        /// - NewClientsThisMonth: The count of unique clients who have made their first appointment with the business this month.
        /// </returns>
        /// <remarks>
        /// - If today is the first day of the month, only the average rating is calculated, and other metrics are skipped.
        /// - The method uses <see cref="DateOnly"/> for date calculations to ensure accuracy without time components.
        /// - LINQ queries are used to filter and calculate the required data efficiently.
        /// </remarks>
        public async Task<BusinessStatisticsViewModel> GetBusinessStatisticsAsync(string userId)
        {
            try
            {


                int businessId = await GetBusinessIdFromUserIdAsync(userId);
                BusinessStatisticsViewModel model = new();
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                DateOnly yesterday = today.AddDays(-1);

                var query = sqlService
                    .AllReadOnly<Appointment>()
                    .Where(a => a.BusinessServiceProviderId == businessId
                                && a.Status == AppointmentStatus.Confirmed
                                && a.Day <= yesterday);

                int totalApp = await query.Where(a => a.RatingId != null).CountAsync();
                int sumOfRating = await query.Where(a => a.RatingId != null).SumAsync(a => a.Rating.Evaluation);
                model.AverageRating = totalApp == 0 || sumOfRating == 0 ? 0 : (double)sumOfRating / totalApp;

                if (today.Day == 1)
                {
                    return model;
                }

                DateOnly beginningOfMonth = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);

                int tempTotalMonthly = await query.Where(a => a.Day >= beginningOfMonth).CountAsync();
                model.TotalMonthlyAppointments = tempTotalMonthly;
                var thisMonthAppointments = query
                                .Where(a => a.Day >= beginningOfMonth && a.Day <= yesterday);
                int tempNewClients = thisMonthAppointments
                    .Select(a => a.UserId)
                    .Distinct()
                    .Count(userId => query.Count(a => a.UserId == userId) == 1);
                model.NewClientsThisMonth = tempNewClients;
                return model;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while counting appointments or calculating statistics.", ex);
            }
        }

        /// <summary>
        /// Retrieves the working schedule of a business service provider based on the provided user ID.
        /// Supports both integer (provider ID) and string (application user ID) types.
        /// </summary>
        /// <typeparam name="T">The type of the user ID (int or string).</typeparam>
        /// <param name="userId">The user ID of the business service provider.</param>
        /// <returns>
        /// A list of <see cref="DailyScheduleViewModel"/> representing the working schedule of the provider,
        /// or null if no matching provider is found.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the user ID type is unsupported.</exception>
        public async Task<List<DailyScheduleViewModel>>? GetUserWorkingShedulesAsync<T>(T userId)
        {

            var query = sqlService.AllReadOnly<BusinessServiceProvider>();

            query = userId switch
            {
                int id => query.Where(b => b.Id == id),
                string appUserId => query.Where(b => b.ApplicationUserId == appUserId),
                _ => throw new ArgumentException("Unsupported user ID type.")
            };

            var schedule = await query
                .Select(b => new BusinessProviderFormModel()
                {
                    ExistedSchedule = b.WorkingSchedules
                        .Select(w => new DailyScheduleViewModel()
                        {
                            Day = w.Day,
                            IsDayOff = w.IsDayOff,
                            StartTime = w.StartTime,
                            EndTime = w.EndTime,
                        }).ToList(),
                })
                .FirstOrDefaultAsync() 
                        ?? throw new NullReferenceException(nameof(BusinessServiceProvider)); ;

            return schedule.ExistedSchedule;
        }

       
    }
}
