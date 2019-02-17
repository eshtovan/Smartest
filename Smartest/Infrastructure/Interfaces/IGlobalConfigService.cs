namespace Smartest.Infrastructure.Interfaces
{
    public interface IGlobalConfigService
    {
        void Update(string SettingName, object value);
        object Get(string SettingName);
    }
}