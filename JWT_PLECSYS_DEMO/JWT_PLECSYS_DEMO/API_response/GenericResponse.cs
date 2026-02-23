namespace JWT_PLECSYS_DEMO.API_response
{
    public class GenericResponse<T>(T? data, bool success, string message)
    {
        public T? Data { get; set; } = data;

        public bool Success { get; set; } = success;

        public string Message { get; set; } = message;
    }
}
