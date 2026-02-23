using APPLICATION.Use_cases.Handlers;
using APPLICATION.Use_cases.User_cases.Create;
using DOMAIN.Entities;
using FastEndpoints;
using JWT_PLECSYS_DEMO.API_response;
using JWT_PLECSYS_DEMO.Data_estructures;

namespace JWT_PLECSYS_DEMO.Endpoints
{
    public class RegisterUser(CreateUserHandler handler) : Endpoint<CreateUserRequest, CreateUserResponse>
    {
        public override void Configure()
        {
            Post("/user/registration");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user_request = new NewUserRequest
                {
                    Username = request.Username,
                    Password = request.Password,
                    Role = request.Role,
                    Country_id = request.Country_id
                };

                var created = await handler.CreateUser(user_request);
                if (created is null)
                {
                    await ResponseBuilder.BuildResponse<CreateUserResponse>(null, false, "No se ha podido crear al usuario",
                        HttpContext, StatusCodes.Status400BadRequest, cancellationToken);
                }
                var success = new CreateUserResponse
                {
                    User = new User
                    {
                        Username = user_request.Username,
                        Role = user_request.Role,
                        Country = created.Country
                    }
                };
                await ResponseBuilder.BuildResponse<CreateUserResponse>(success, true, "Usuario creado exitosamente",
                    HttpContext, StatusCodes.Status200OK, cancellationToken);
            } catch (Exception ex)
            {
                await ResponseBuilder.BuildResponse<CreateUserResponse>(null, false, 
                    "Ha ocurrido un error durante el proceso, intente nuevamente más tarde: " + ex.Message,
                    HttpContext, StatusCodes.Status500InternalServerError, cancellationToken);
            }
        }
    }
}
