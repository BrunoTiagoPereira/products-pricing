namespace ProductsPricing.Api.ApiResponses
{
    public class ApiResponse
    {
        private ApiResponse(bool isSuccess, object data, string stackTrace, IEnumerable<string> errorMessages)
        {
            IsSuccess = isSuccess;
            Data = data;
            StackTrace = stackTrace;
            ErrorMessages = errorMessages;
        }
        public bool IsSuccess { get; private set; }
        public object Data { get; private set; }
        public bool HasException => !string.IsNullOrWhiteSpace(StackTrace);
        public string StackTrace { get; private set; }
        public IEnumerable<string> ErrorMessages { get; private set; }
        public static ApiResponse Error(IEnumerable<string> errorMessages, string stackTrace) => new(false, null, stackTrace, errorMessages);
        public static ApiResponse Error(IEnumerable<string> errorMessages) => new(false, null, null, errorMessages);
        public static ApiResponse Success(object data) => new(true, data, null, null);
        public static ApiResponse Success() => new(true, "Ok", null, null);
    }
}
