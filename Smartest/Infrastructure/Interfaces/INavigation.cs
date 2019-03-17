using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartest.Infrastructure.Objects;

namespace Smartest.Infrastructure.Interfaces
{
    public interface INavigation
    {
        void GoBack();
        void GoToPage(string fromPage, Enums.Pages page);
         
    }
}
