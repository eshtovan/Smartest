using Smartest.Infrastructure.Interfaces;

namespace Smartest.Models
{
    public class ProjectSettings : ISettings
    {
        public object this[string propertyName]
        {
            get
            {
                return Properties.Settings.Default[propertyName];
            }
            set
            {
                Properties.Settings.Default[propertyName] = value;
            }
        }

        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
