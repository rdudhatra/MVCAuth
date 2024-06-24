using Microsoft.AspNetCore.Mvc;
using MVCAuth.Core.Services.Interface;
using MVCAuth.Data.Models;
using System.Diagnostics;

namespace MVCAuth.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingServices _bookingServices;
        public BookingController(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }
        public IActionResult Booking()
        {
            return View();
        }
        public IActionResult GetCalendarData()
        {

            List<AvailibilityDto> data = new List<AvailibilityDto>();

            //Statically create list and add data  
            AvailibilityDto infoObj1 = new AvailibilityDto();
            infoObj1.Id = 1;
            infoObj1.Title = "I am available";
            infoObj1.Desc = "Description 1";
            infoObj1.Start_Date = "2020-08-16 22:37:22.467";
            infoObj1.End_Date = "2020-08-16 23:30:22.467";
            data.Add(infoObj1);

            AvailibilityDto infoObj2 = new AvailibilityDto();
            infoObj2.Id = 2;
            infoObj2.Title = "Available";
            infoObj2.Desc = "Description 1";
            infoObj2.Start_Date = "2020-08-17 10:00:22.467";
            infoObj2.End_Date = "2020-08-17 11:00:22.467";
            data.Add(infoObj2);


            AvailibilityDto infoObj3 = new AvailibilityDto();
            infoObj3.Id = 3;
            infoObj3.Title = "Meeting";
            infoObj3.Desc = "Description 1";
            infoObj3.Start_Date = "2020-08-18 07:30:22.467";
            infoObj3.End_Date = "2020-08-18 08:00:22.467";
            data.Add(infoObj3);

            return new JsonResult(data);

        }

        [HttpGet("/booking/GetBookings")]
        public async Task<JsonResult> GetBookings()
        {
            var result = await _bookingServices .GettAllBooking();
            return new JsonResult(result);
        }

        [HttpPost("/booking/SaveBooking")]
        public async Task<JsonResult> SaveBooking(Booking e)
            {
            var status = false;
            try
            {
                await _bookingServices.SaveBooking(e);
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return new JsonResult(status);
        }

        [HttpPost("/booking/DeleteEvent")]
        public async Task<JsonResult> DeleteEvent(int bookingId)
        {
            bool status = false;
            try
            {
                status = await _bookingServices.RemoveBooking(bookingId);
            }
            catch
            {
                status = false;
            }
            return new JsonResult(status);
        }

    }
}
