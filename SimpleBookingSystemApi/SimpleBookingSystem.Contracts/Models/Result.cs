namespace SimpleBookingSystem.Contracts.Models
{
    public class Result
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public string? ErrorMessage { get; }

        protected Result(bool isSuccess, string? errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Success() => new(isSuccess: true, errorMessage: string.Empty);

        public static Result Failed(string errorMessage) => new(isSuccess: false, errorMessage: errorMessage);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(T value) : base(isSuccess: true, errorMessage: null) => Value = value;

        private Result(string? errorMessage) : base(isSuccess: false, errorMessage: errorMessage) => Value = default;

        public static Result<T> Success(T value) => new(value: value);

        public static new Result<T> Failed(string errorMessage) => new(errorMessage: errorMessage);
    }
}
