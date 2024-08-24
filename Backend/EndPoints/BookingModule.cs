using Backend.Facories;
using Backend.Models;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace Backend.EndPoints
{
    public class BookingModule : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapPost("/BookSlot", async (ISlotBookingFactory slotBookingFactory, [FromBody] SlotBooking slot) =>
            {
                var res = await slotBookingFactory.CreateSlotBooking(slot);
                return Results.Ok(slot);
            });
            app.MapGet("/GetAllSlots", async (ISlotBookingFactory slotBookingFactory) =>
            {
                var res = await slotBookingFactory.GetAllSlotsAsync();
                return Results.Ok(res);
            });
            app.MapPost("/Exit", async (ISlotBookingFactory slotBookingFactory, [FromBody] SlotBooking bookingDetails1) =>
            {
                await slotBookingFactory.ExitSlot(bookingDetails1);
            });

            app.MapGet("/GetBookingDetails", async (ISlotBookingFactory slotBookingFactory, string bookingNumber) =>
            {
                var res = await slotBookingFactory.GetBookingDetailsByBookingId(bookingNumber);
                return Results.Ok(res);
            });
            app.MapPost("/LockSpot/{slotId}", async (ISlotBookingFactory slotBookingFactory, string slotId) =>
            {       
                await slotBookingFactory.LockSpot(slotId);
                return Results.Ok();
    }       );

            app.MapPost("/UnlockSpot/{slotId}", async (ISlotBookingFactory slotBookingFactory, string slotId, [FromQuery] bool isBookingConfirmed) =>
            {
                await slotBookingFactory.UnlockSpot(slotId, isBookingConfirmed);
                return Results.Ok();
            });

        }
    }

}