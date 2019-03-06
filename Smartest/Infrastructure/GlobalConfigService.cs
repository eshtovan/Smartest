using Smartest.Infrastructure.Interfaces;
using System;

namespace Smartest.Infrastructure
{
    public class GlobalConfigService : IGlobalConfigService
    {
        protected ISettings Settings;

        public GlobalConfigService(ISettings settings)
        {
            Settings = settings;
        }

        public void Update(string settingName, object value)
        {
            if (String.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("Setting name must be provided");

            var setting = Settings[settingName];

            if (setting == null)
            {
                throw new ArgumentException("Setting " + settingName + " not found.");
            }
            else if (setting.GetType() != value.GetType())
            {
                throw new InvalidCastException("Unable to cast value to " + setting.GetType());
            }
            else
            {
                Settings[settingName] = value;
                Settings.Save();
            }

        }

        public object Get(string settingName)
        {
            if (String.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("Setting name must be provided");

            return Settings[settingName];
        }
    }
}