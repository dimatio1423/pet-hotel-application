using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.DashboardService
{
    public interface IDashboardService
    {
        int GetTotalUsers();
        int GetTotalBookings();
        int GetTotalServices();
        int GetTotalAccommodations();
        int[] GetFeedbackCountsByRating();
    }
}
