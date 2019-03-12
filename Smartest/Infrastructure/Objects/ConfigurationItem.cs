using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartest.Infrastructure.Objects
{
    public class ConfigurationItem
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ConfigurationItem(string key,string value)
        {
            Key = key;
            Value = value;
        }
    }
}
