namespace SlackTestWebApi.Domain.Dtos
{
    public class BaseResponseDto<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }
        public object Errors { get; set; }
        public BaseResponseDto() { }
        public BaseResponseDto(string message, T result, object errors)
        {
            Message = message;
            Result = result;
            Errors = errors;
        }
    }
}
