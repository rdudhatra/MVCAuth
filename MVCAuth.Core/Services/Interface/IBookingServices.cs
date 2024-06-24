using MVCAuth.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAuth.Core.Services.Interface
{
    public interface IBookingServices
    {
        Task<List<Booking>> GettAllBooking();
        Task<Booking> SaveBooking(Booking booking);
        Task<bool> RemoveBooking(int bookingId);
    }
}
