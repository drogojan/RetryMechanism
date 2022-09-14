namespace RetryMechanism
{
    public class RetryMechanism
    {
        public static T WaitAndRetry<T>(Func<T> function, int retries, Func<int, int> waitTimeProvider, Action<int> wait)
        {
            try
            {
                return function();
            }
            catch
            {
                return function();
            }
        }
    }
}