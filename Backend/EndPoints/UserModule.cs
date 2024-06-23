using Backend.Facories;
using Backend.Models;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace Backend.EndPoints
{
    public class UserModule : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/CreateUser", async (IUserFactory userFactory, [FromBody] User user) =>
            {
                var res = await userFactory.CreateUserAsync(user);
                return Results.Ok(res);
            });
        }
    }

}