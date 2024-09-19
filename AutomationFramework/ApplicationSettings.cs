namespace AutomationFramework
{
    public static class ApplicationSettings
    {
        public static bool NeedMaximizeWindow { get; set; }
        public static bool Headless { get; set; }
        public static int DefaulCommandTimeout { get; set; }
        public static int PageLoadTimeout { get; set; }
        public static int ImplicitWaitTimeout { get; set; }
    }
}
