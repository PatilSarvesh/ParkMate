using System.Text;
using Backend.Facories;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

//
var identityUrl = builder.Configuration.GetSection("IdentityUrl").ToString();

builder.Services.AddAuthentication(x =>
{

    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.Authority = identityUrl;
    x.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtKey").ToString())),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

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
    return Results.Ok(res);
});
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
// Use CORS middleware
app.UseCors("CorsPolicy");
app.Run();
