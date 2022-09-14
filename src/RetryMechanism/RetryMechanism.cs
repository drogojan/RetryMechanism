namespace RetryMechanism
{
    public class RetryMechanism
    {
        public static T WaitAndRetry<T>(Func<T> function, int retries, Func<int, int> waitTimeProvider, Action<int> wait)
        {
            var retry = 0;
            do
            {
                try
                {
                    return function();
                }
                catch
                {
                    if (retry == retries)
                    {
                        throw;
                    }
                    wait(waitTimeProvider(retry));
                    retry++;
                }
            } while (true);
        }
    }
}