using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight;
using Smartest.Properties;

namespace Smartest.ViewModels
{
   public class BaseViewModel : ViewModelBase
    {

        /// <summary>
        /// This gives us the ReSharper option to transform an autoproperty into a property with change notification
        /// Also leverages .net 4.5 callermembername attribute
        /// </summary>
        /// <param name="property">name of the property</param>
        [NotifyPropertyChangedInvocator]
        public override void RaisePropertyChanged([CallerMemberName]string property = "")
        {
            base.RaisePropertyChanged(property);
        }

        ///// <summary>
        ///// This gives us the ReSharper option to transform an autoproperty into a property with change notification
        ///// Also leverages .net 4.5 callermembername attribute
        ///// </summary>
        ///// <param name="property">name of the property</param>
        //[NotifyPropertyChangedInvocator]
        //protected override void RaisePropertyChanging([CallerMemberName]string property = "")
        //{
        //    base.RaisePropertyChanging(property);
        //}
    }
}