namespace sentirsebien_backend.Domain.Shared
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static Result Success(object data = null)
        {
            return new Result { IsSuccess = true, Data = data };
        }

        public static Result Failure(string message)
        {
            return new Result { IsSuccess = false, Message = message };
        }
    }

}
