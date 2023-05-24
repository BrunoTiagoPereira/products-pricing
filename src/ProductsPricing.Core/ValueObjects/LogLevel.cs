namespace ProductsPricing.Core.ValueObjects
{
    public class LogLevel : Enumeration<int>
    {
        private LogLevel(string name, int value) : base(name, value)
        {
        }

        public static LogLevel Information() => new("Information", 1);
        public static LogLevel Warning() => new("Warning", 2);
        public static LogLevel Error() => new("Error", 3);
        public bool IsInformation() => Value == 1;
        public bool IsWarning() => Value == 2;
        public bool IsError() => Value == 3;
    }
}
