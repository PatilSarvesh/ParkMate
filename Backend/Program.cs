using Backend.Facories;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS services
builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    });


builder.Services.Configure<MongoDBSettings>(
                builder.Configuration.GetSection("MongoDBSettings")
                );
builder.Services.Configure<MongoCollections>(
    builder.Configuration.GetSection("MongoCollections")
    );


//Services    
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISlotBookingService, SlotBookingService>();

//Factories
builder.Services.AddScoped<IUserFactory, UserFactory>();
builder.Services.AddScoped<ISlotBookingFactory, SlotBookingFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/BookSlots", async () =>
{
    return Results.Ok("Okay Sarvesh");
});

app.MapPost("/CreateUser", async (IUserFactory userFactory, [FromBody] User user) =>
{
    var res = await userFactory.CreateUserAsync(user);
    return Results.Ok(user);
});
app.MapPost("/BookSlot", async (ISlotBookingFactory slotBookingFactory, [FromBody] SlotBooking slot) =>
{
    var res = await slotBookingFactory.CreateSlotBooking(slot);
    return Results.Ok(slot);
});
app.MapGet("/GetAllSlots", async (ISlotBookingFactory slotBookingFactory) =>{
    var res = await slotBookingFactory.GetAllSlotsAsync();
    return Results.Ok(res);
});
app.MapGet("/Exit", async (ISlotBookingFactory slotBookingFactory, string BookingId) =>  {

});

app.MapGet("/GetBookingDetails", async (ISlotBookingFactory slotBookingFactory, string bookingNumber) =>{
    var res = await slotBookingFactory.GetBookingDetailsByBookingId(bookingNumber);
    return Results.Ok(res);
});
// Use CORS middleware
app.UseCors("CorsPolicy");
app.Run();
