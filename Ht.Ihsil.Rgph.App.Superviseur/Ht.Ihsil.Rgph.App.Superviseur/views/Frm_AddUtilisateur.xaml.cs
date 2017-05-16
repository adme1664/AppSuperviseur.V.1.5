using Ht.Ihsil.Rgph.App.Superviseur.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ht.Ihsil.Rgph.App.Superviseur.views
{
    /// <summary>
    /// Logique d'interaction pour addUtilisateur.xaml
    /// </summary>
    public partial class addUtilisateur : UserControl
    {
        private UtilisateurViewModel view;
        public addUtilisateur()
        {
            InitializeComponent();
            DataContext = new UtilisateurViewModel();
            
        }
    }
}
