namespace JWT_PLECSYS_DEMO.Data_estructures
{
    public class CreateUserRequest
    {
        public required string Username { get; set; }

        public required string Password { get; set; }

        public required string Role { get; set; }

        public required int Country_id { get; set; }
    }
}
