using Smartest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartest.Infrastructure
{
    public class GlobalConfigService : IGlobalConfigService
    {
        protected ISettings _Settings;

        public GlobalConfigService(ISettings Settings)
        {
            _Settings = Settings;
        }

        public void Update(string SettingName, object value)
        {
            if (String.IsNullOrEmpty(SettingName))
                throw new ArgumentNullException("Setting name must be provided");

            var Setting = _Settings[SettingName];

            if (Setting == null)
            {
                throw new ArgumentException("Setting " + SettingName + " not found.");
            }
            else if (Setting.GetType() != value.GetType())
            {
                throw new InvalidCastException("Unable to cast value to " + Setting.GetType());
            }
            else
            {
                _Settings[SettingName] = value;
                _Settings.Save();
            }

        }

        public object Get(string SettingName)
        {
            if (String.IsNullOrEmpty(SettingName))
                throw new ArgumentNullException("Setting name must be provided");

            return _Settings[SettingName];
        }
    }
}