using System.Text;
using Backend.Facories;
using Backend.Models;
using Backend.Services;
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();

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

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

//Services    
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISlotBookingService, SlotBookingService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

//Factories
builder.Services.AddScoped<IUserFactory, UserFactory>();
builder.Services.AddScoped<ISlotBookingFactory, SlotBookingFactory>();
builder.Services.AddScoped<IPaymentFactory, PaymentFactory>();

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


// Use CORS middleware
app.UseCors("CorsPolicy");

app.MapCarter();
app.Run();
