namespace AutomationFramework.Helpers
{
    public static class WaitHelper
    {
        public static bool WaitUntilCondition(Func<bool> action, TimeSpan timeout, int frequencyInSeconds = 1)
        {
            bool result = false;
            var startTime = (uint)Environment.TickCount;

            do
            {
                try
                {
                    result = action();
                    if (result)
                    {
                        return result;
                    }
                }
                catch (Exception) { }
                Thread.Sleep(TimeSpan.FromSeconds(frequencyInSeconds));
            }
            while (timeout.TotalMilliseconds >= (uint)Environment.TickCount - startTime);
            return result;
        }

        public static bool WaitUntilCondition(Action action, TimeSpan timeout, int frequencyInSeconds = 1)
        {
            bool result = false;
            var startTime = (uint)Environment.TickCount;

            do
            {
                try
                {
                    action();
                    result = true;
                    break;
                }
                catch { }

                Thread.Sleep(TimeSpan.FromSeconds(frequencyInSeconds));
            }
            while (timeout.TotalMilliseconds >= (uint)Environment.TickCount - startTime);

            return result;
        }
    }
}
