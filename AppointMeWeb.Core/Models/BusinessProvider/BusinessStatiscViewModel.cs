using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointMeWeb.Core.Models.BusinessProvider
{
    public class BusinessStatisticsViewModel
    {
        public int TotalMonthlyAppointments { get; set; }
        public double AverageRating { get; set; }
        public int NewClientsThisMonth { get; set; }
    }
}
