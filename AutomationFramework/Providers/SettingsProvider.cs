using NUnit.Framework;

namespace AutomationFramework.Providers
{
    public static class SettingsProvider
    {
        public static void ReadSettings()
        {
            var modelProps = typeof(ApplicationSettings).GetProperties().ToArray();

            foreach (var prop in modelProps)
            {
                var value = TestContext.Parameters[prop.Name];

                var convertedValue = value == null ? prop.GetValue(null) : Convert.ChangeType(value, prop.PropertyType);
                prop.SetValue(null, convertedValue);
            }
        }
    }
}