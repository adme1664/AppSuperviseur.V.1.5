using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ht.Ihsil.Rgph.App.Superviseur.MVVM
{
    public interface IMessageBoxViewModel
    {
        MessageBoxButtons MessageBoxButton { get; set; }
        MessageBoxIcon MessageBoxImage { get; set; }
        string Title { get; set; }
        string Message { get; set; }
    }
}
