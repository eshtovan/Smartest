using Smartest.Infrastructure.Objects;

namespace Smartest.Utilities
{
    public static class ProjectsData
    {
       public static string CurrentProjectName { get; set; }

       public static string CurrentConfigurationName { get; set; }

       public static PlacedDataItem CurrentDataItem { get; set; }

      // public static string CurrentConfigurationView { get; set; }

        public static object LastPage { get; set; } = null;
    }
}
