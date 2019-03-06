namespace Smartest.Infrastructure.Interfaces
{
    public interface IGlobalConfigService
    {
        void Update(string settingName, object value);
        object Get(string settingName);
    }
}